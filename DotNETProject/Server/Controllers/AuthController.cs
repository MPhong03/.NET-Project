using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using DotNETProject.Server.Data;
using DotNETProject.Shared;
using DotNETProject.Server.Models;
using Azure.Core;
using DotNETProject.Server.Services;

namespace DotNETProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //public static User user = new User();
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;

        public AuthController(ApplicationDbContext context, IConfiguration configuration, EmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            //CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            //user.UserName = request.UserName;
            //user.Email = request.Email;
            //user.PasswordHash = passwordHash;
            //user.PasswordSalt = passwordSalt;

            //return Ok(user);

            if (await _context.Users.AnyAsync(x => x.Email == request.Email))
            {
                return BadRequest("User already exists!");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                createdDate = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                return BadRequest("User not found!");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password!");
            }

            string token;
            long tokenExpired;
            CreateToken(user, out token, out tokenExpired);

            return Ok(new ReponseDto("Login successfully!", token, tokenExpired));
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<string>> forgotPassword(EmailDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                return BadRequest("User not found!");
            }

            // Send OTP

            try
            {
                string otp = GenerateRandomOTP();
                user.Otp = otp;
                user.ResetPasswordExpiry = DateTime.Now.AddMinutes(30);
                await _context.SaveChangesAsync();

                await _emailService.SendOTP(user.Email, otp);

                return Ok("OTP sent successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost("verify-otp")]
        public async Task<ActionResult<string>> verifyOTP(OTPDto request) 
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                return BadRequest("User not found!");
            }

            if (!((user.Otp).Equals(request.Otp)))
            {
                return BadRequest("Wrong OTP!");
            }

            if ((user.ResetPasswordExpiry) < DateTime.Now)
            {
                return BadRequest("OTP has expired");
            }

            user.IsPermit = true;
            
            await _context.SaveChangesAsync();

            return Ok("You have permission to change password!");
        }
        [HttpPost("reset-password")]
        public async Task<ActionResult<string>> resetPassword(ResetPasswordDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                return BadRequest("User not found!");
            }

            if (user.IsPermit == false)
            {
                return BadRequest("You don't have permission to change password!");
            }

            if (!((request.Password).Equals(request.PasswordConfirm)))
            {
                return BadRequest("Wrong password confirm");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            user.IsPermit = false;

            await _context.SaveChangesAsync();

            return Ok("Successfully changed password!");
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
        private void CreateToken(User user, out string jwtToken, out long tokenExpiration)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var expirationDate = DateTime.Now.AddMinutes(30);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expirationDate,
                signingCredentials: creds
                );

            jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            DateTimeOffset e = (DateTimeOffset)expirationDate;
            tokenExpiration = e.ToUnixTimeSeconds();

            //return jwt;
        }

        private string GenerateRandomOTP(int length = 6)
        {
            const string allowedChars = "0123456789";
            Random random = new Random();
            char[] otp = new char[length];

            for (int i = 0; i < length; i++)
            {
                otp[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }

            return new string(otp);
        }
    }
}
