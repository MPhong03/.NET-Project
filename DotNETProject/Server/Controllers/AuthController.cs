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
        [HttpGet("detail")]
        public async Task<ActionResult<UserDetailDto>> GetUser(string email)
        {
            var user = await _context.Users
                .Include(user => user.Role)
                .Include(user => user.Films)
                .FirstOrDefaultAsync(user => user.Email == email);

            if (user == null)
            {
                return NotFound($"User with Id = {email} doesn't exist!");
            }

            var dto = new UserDetailDto();
            dto.Id = user.Id;
            dto.Email = user.Email;
            dto.Username = user.UserName;
            dto.createdDate = user.createdDate;
            dto.RoleName = user.Role.Name;
            foreach (var item in user.Films)
            {
                var type = (item.GetType().Equals(typeof(Movie))) ? "movie" : "tv";
                dto.SavedFilms.Add(new FilmDto
                {
                    Id = item.Id,
                    BackgroundUrl = item.BackgroundUrl,
                    Description = item.Description,
                    LogoUrl = item.LogoUrl,
                    PosterUrl = item.PosterUrl,
                    Name = item.Name,
                    isActiveBanner = item.isActiveBanner,
                    Type = type
                });
            }

            return Ok(dto);
        }

        [HttpPost("save")]
        public async Task<ActionResult<string>> SaveFilmById(SaveFilmDto request)
        {
            var film = await _context.Films.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (film == null)
            {
                return NotFound($"Film with Id = {request.Id} not found");
            }
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == request.Email);
            if (user == null)
            {
                return NotFound($"User not found");
            }
            var message = "";
            if (user.Films.Contains(film))
            {
                user.Films.Remove(film);
                message = "DA XOA KHOI BO SUU TAP";
            }
            else
            {
                user.Films.Add(film);
                message = "DA LUU VAO BO SUU TAP";
            }

            await _context.SaveChangesAsync();

            return Ok(message);
        }

        [HttpGet("checkSaved/{id}")]
        public async Task<ActionResult<bool>> CheckSaved(int id, string email)
        {
            var user = await _context.Users
                .Include(user => user.Films)
                .FirstOrDefaultAsync(user => user.Email == email);

            if (user == null)
            {
                return NotFound("User not found!");
            }

            var chk = user.Films.Any(film => film.Id == id);

            return Ok(chk);
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
                createdDate = DateTime.Now,
                Role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == 1)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {

            var user = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == request.Email);

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
                return StatusCode(500, "Internal Server Error: " + ex);
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
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
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
