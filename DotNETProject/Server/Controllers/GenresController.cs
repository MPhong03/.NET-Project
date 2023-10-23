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
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetGenres()
        {
            if (_context.Genres == null)
            {
                return NotFound();
            }

            var genreEntities = await _context.Genres
                .Include(genre => genre.FilmGenres)
                .ThenInclude(filmGenre => filmGenre.Film)
                .ToListAsync();

            var genreDtos = new List<GenreDto>();

            foreach (var genreEntity in genreEntities)
            {
                var genreDto = new GenreDto()
                {
                    Id = genreEntity.Id,
                    Name = genreEntity.Name
                };

                foreach (var filmgenre in genreEntity.FilmGenres)
                {
                    var filmGenreDto = new FilmGenreDto()
                    {
                        Film = new FilmDto()
                        {
                            Name = filmgenre.Film.Name
                        }
                    };
                    genreDto.FilmGenres.Add(filmGenreDto);
                }

                genreDtos.Add(genreDto);
            }

            return genreDtos;
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetGenre(int id)
        {
            if (_context.Genres == null)
            {
                return NotFound();
            }
            var genreEntity = await _context.Genres
                .Include(genre => genre.FilmGenres)
                .ThenInclude(filmGenre => filmGenre.Film)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (genreEntity == null)
            {
                return NotFound();
            }

            var genreDto = new GenreDto()
            {
                Id = genreEntity.Id,
                Name = genreEntity.Name
            };

            foreach (var filmGenre in genreEntity.FilmGenres)
            {
                var filmGenreDto = new FilmGenreDto()
                {
                    Film = new FilmDto()
                    {
                        Name = filmGenre.Film.Name,
                        Id = filmGenre.Film.Id,
                        PosterUrl = filmGenre.Film.PosterUrl
                    },
                };
                genreDto.FilmGenres.Add(filmGenreDto);
            }

            return genreDto;
        }

        // PUT: api/Genres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, GenreDto genreDto)
        {
            if (id != genreDto.Id)
            {
                return BadRequest();
            }

            Genre genre = new Genre();
            genre.Id = genreDto.Id;
            genre.Name = genreDto.Name;

            _context.Entry(genre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
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

        // POST: api/Genres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Genre>> PostGenre(GenreDto genreDto)
        {
            if (_context.Genres == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Genres'  is null.");
            }

            Genre genre = new Genre();
            genre.Id = genreDto.Id;
            genre.Name = genreDto.Name;

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenre", new { id = genre.Id }, genre);
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            if (_context.Genres == null)
            {
                return NotFound();
            }
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenreExists(int id)
        {
            return (_context.Genres?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
