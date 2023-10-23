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
    public class DirectorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DirectorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Directors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectorDto>>> GetDirectors()
        {
            if (_context.Directors == null)
            {
                return NotFound();
            }

            var directorEntities = await _context.Directors
                .Include(director => director.FilmDirectors)
                .ThenInclude(filmDirector => filmDirector.Film)
                .ToListAsync();

            var directorDtos = new List<DirectorDto>();

            foreach (var directorEntity in directorEntities)
            {
                var directorDto = new DirectorDto()
                {
                    Id = directorEntity.Id,
                    Name = directorEntity.Name,
                    Description = directorEntity.Description,
                    BirthDate = directorEntity.BirthDate,
                    AvatarUrl = directorEntity.AvatarUrl,
                    Gender = directorEntity.Gender,
                    Nation = directorEntity.Nation,
                };

                foreach (var filmDirector in directorEntity.FilmDirectors)
                {
                    var filmDirectorDto = new FilmDirectorDto()
                    {
                        Film = new FilmDto()
                        {
                            Name = filmDirector.Film.Name
                        }
                    };
                    directorDto.FilmDirectors.Add(filmDirectorDto);
                }

                directorDtos.Add(directorDto);
            }

            return directorDtos;
        }

        // GET: api/Directors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectorDto>> GetDirector(int id)
        {
            if (_context.Directors == null)
            {
                return NotFound();
            }
            var directorEntity = await _context.Directors
                .Include(director => director.FilmDirectors)
                .ThenInclude(filmDirector => filmDirector.Film)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (directorEntity == null)
            {
                return NotFound();
            }

            var directorDto = new DirectorDto()
            {
                Id = directorEntity.Id,
                Name = directorEntity.Name,
                Description = directorEntity.Description,
                BirthDate = directorEntity.BirthDate,
                AvatarUrl = directorEntity.AvatarUrl,
                Gender = directorEntity.Gender,
                Nation = directorEntity.Nation,
            };

            foreach (var filmDirector in directorEntity.FilmDirectors)
            {
                var filmDirectorDto = new FilmDirectorDto()
                {
                    Film = new FilmDto()
                    {
                        Name = filmDirector.Film.Name,
                        Id = filmDirector.Film.Id,
                        PosterUrl = filmDirector.Film.PosterUrl
                    },
                };
                directorDto.FilmDirectors.Add(filmDirectorDto);
            }

            return directorDto;
        }

        // PUT: api/Directors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDirector(int id, DirectorDto directorDto)
        {
            if (id != directorDto.Id)
            {
                return BadRequest();
            }

            Director director = new Director();
            director.Id = directorDto.Id;
            director.Name = directorDto.Name;
            director.Description = directorDto.Description;
            director.AvatarUrl = directorDto.AvatarUrl;
            director.BirthDate = directorDto.BirthDate;
            director.Gender = directorDto.Gender;
            director.Nation = directorDto.Nation;

            _context.Entry(director).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectorExists(id))
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

        // POST: api/Directors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Director>> PostDirector(DirectorDto directorDto)
        {
            if (_context.Directors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Directors'  is null.");
            }

            Director director = new Director();
            director.Id = directorDto.Id;
            director.Name = directorDto.Name;
            director.Description = directorDto.Description;
            director.AvatarUrl = directorDto.AvatarUrl;
            director.BirthDate = directorDto.BirthDate;
            director.Gender = directorDto.Gender;
            director.Nation = directorDto.Nation;

            _context.Directors.Add(director);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDirector", new { id = director.Id }, director);
        }

        // DELETE: api/Directors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            if (_context.Directors == null)
            {
                return NotFound();
            }
            var director = await _context.Directors.FindAsync(id);
            if (director == null)
            {
                return NotFound();
            }

            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DirectorExists(int id)
        {
            return (_context.Directors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
