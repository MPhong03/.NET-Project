using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNETProject.Server.Data;
using DotNETProject.Server.Models;
using DotNETProject.Shared;

namespace DotNETProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CastsController(ApplicationDbContext context)
        {
            _context = context;
        }   

        // GET: api/Casts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CastDto>>> GetCasts()
        {
            if (_context.Casts == null)
            {
                return NotFound();
            }

            var castEntities = await _context.Casts
                .Include(cast => cast.FilmCasts)
                .ThenInclude(filmCast => filmCast.Film)
                .ToListAsync();

            var castDtos = new List<CastDto>();

            foreach (var castEntity in castEntities)
            {
                var castDto = new CastDto()
                {
                    Id = castEntity.Id,
                    Name = castEntity.Name,
                    Description = castEntity.Description,
                    BirthDate = castEntity.BirthDate,
                    AvatarUrl = castEntity.AvatarUrl,
                    Gender = castEntity.Gender,
                    Nation = castEntity.Nation,
                };

                foreach (var filmCast in castEntity.FilmCasts)
                {
                    var filmCastDto = new FilmCastDto()
                    {
                        Film = new FilmDto()
                        {
                            Name = filmCast.Film.Name
                        },
                        Role = filmCast.Role
                    };
                    castDto.FilmCasts.Add(filmCastDto);
                }

                castDtos.Add(castDto);
            }

            return castDtos;
        }

        // GET: api/Casts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CastDto>> GetCast(int id)
        {
            if (_context.Casts == null)
            {
                return NotFound();
            }

            var castEntity = await _context.Casts
                .Include(cast => cast.FilmCasts)
                .ThenInclude(filmCast => filmCast.Film)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (castEntity == null)
            {
                return NotFound();
            }

            var castDto = new CastDto()
            {
                Id = castEntity.Id,
                Name = castEntity.Name,
                Description = castEntity.Description,
                BirthDate = castEntity.BirthDate,
                AvatarUrl = castEntity.AvatarUrl,
                Gender = castEntity.Gender,
                Nation = castEntity.Nation,
            };

            foreach (var filmCast in castEntity.FilmCasts)
            {
                var filmCastDto = new FilmCastDto()
                {
                    Film = new FilmDto()
                    {
                        Name = filmCast.Film.Name,      
                        Id = filmCast.Film.Id,
                        PosterUrl = filmCast.Film.PosterUrl
                    },
                    Role = filmCast.Role
                };
                castDto.FilmCasts.Add(filmCastDto);
            }

            return castDto;
        }

        // PUT: api/Casts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCast(int id, CastDto castDto)
        {
            if (id != castDto.Id)
            {
                return BadRequest();
            }

            Cast cast = new Cast();
            cast.Id = castDto.Id;
            cast.Name = castDto.Name;
            cast.Description = castDto.Description;
            cast.AvatarUrl = castDto.AvatarUrl;
            cast.Nation = castDto.Nation;
            cast.BirthDate = castDto.BirthDate;
            cast.Gender = castDto.Gender;

            _context.Entry(cast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CastExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Casts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CastDto>> PostCast(CastDto castDto)
        {
            if (_context.Casts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Casts'  is null.");
            }

            Cast cast = new Cast();
            cast.Id = castDto.Id;
            cast.Name = castDto.Name;
            cast.Description = castDto.Description;
            cast.AvatarUrl = castDto.AvatarUrl;
            cast.Nation = castDto.Nation;
            cast.BirthDate = castDto.BirthDate;
            cast.Gender = castDto.Gender;

            _context.Casts.Add(cast);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCast", new { id = cast.Id }, cast);
        }

        // DELETE: api/Casts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCast(int id)
        {
            if (_context.Casts == null)
            {
                return NotFound();
            }
            var cast = await _context.Casts.FindAsync(id);
            if (cast == null)
            {
                return NotFound();
            }

            _context.Casts.Remove(cast);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CastExists(int id)
        {
            return (_context.Casts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
