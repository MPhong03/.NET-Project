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
using Microsoft.AspNetCore.Authorization;

namespace DotNETProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FilmsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetFilmType(int id)
        {
            if (_context.Films == null)
            {
                return NotFound();
            }
            var film = await _context.Films.FindAsync(id);

            string flag = "NOTFOUND";

            if (film.GetType() == typeof(Movie))
            {
                flag = "MOVIE";
            } else if (film.GetType() == typeof(TVSeries))
            {
                flag = "TV";
            }

            if (film == null)
            {
                return NotFound();
            }

            return flag;
        }

        [HttpGet("years")]
        public async Task<ActionResult<List<string>>> GetFilmYears()
        {
            if (_context.Films == null)
            {
                return NotFound();
            }
            var film = await _context.Films.ToListAsync();

            var years = film.Select(item => item.ReleaseYear)
                            .Distinct()
                            .OrderByDescending(year => year)
                            .ToList();

            if (film == null)
            {
                return NotFound();
            }

            return years;
        }

        // GET: api/Films
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmDto>>> GetFilms()
        {
            if (_context.Films == null)
            {
                return NotFound();
            }

            var list = await _context.Films
                .Include(f => f.FilmGenres)
                .ThenInclude(filmGenre => filmGenre.Genre)
                .Include(f => f.Nation)
                .ToListAsync();
            var listDto = new List<FilmDto>();

            foreach (var item in list)
            {
                var type = (item.GetType().Equals(typeof(Movie))) ? "movie" : "tv";
                var film = new FilmDto
                {
                    Id = item.Id,
                    BackgroundUrl = item.BackgroundUrl,
                    Description = item.Description,
                    LogoUrl = item.LogoUrl,
                    PosterUrl = item.PosterUrl,
                    Name = item.Name,
                    isActiveBanner = item.isActiveBanner,
                    Type = type,
                    ReleaseYear = item.ReleaseYear,
                    View = item.View
                };

                if (type.Equals("tv"))
                {
                    var tvSeries = await _context.TVSeries
                        .Include(tv => tv.episodes)
                        .FirstOrDefaultAsync(tv => tv.Id == item.Id);

                    if (tvSeries.episodes.Any())
                    {
                        var mostViewedEpisode = tvSeries.episodes.OrderByDescending(e => e.View).First();

                        film.View = mostViewedEpisode.View;
                    }
                }

                film.Nation.Name = item.Nation.Name;

                foreach (var filmGenre in item.FilmGenres)
                {
                    var filmGenreDto = new FilmGenreDto()
                    {
                        Id = filmGenre.Id,
                        Genre = new GenreDto()
                        {
                            Name = filmGenre.Genre.Name,
                            Id = filmGenre.Genre.Id
                        }
                    };
                    film.FilmGenres.Add(filmGenreDto);
                }


                listDto.Add(film);
            }

            return listDto;
        }

        // GET: api/Films/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Film>> GetFilm(int id)
        //{
        //    if (_context.Films == null)
        //    {
        //        return NotFound();
        //    }
        //    var film = await _context.Films.FindAsync(id);

        //    if (film == null)
        //    {
        //        return NotFound();
        //    }

        //    return film;
        //}

        // PUT: api/Films/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilm(int id, Film film)
        {
            if (id != film.Id)
            {
                return BadRequest();
            }

            _context.Entry(film).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
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

        // POST: api/Films
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPost]
        public async Task<ActionResult<Film>> PostFilm(Film film)
        {
            if (_context.Films == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Films'  is null.");
            }
            _context.Films.Add(film);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilm", new { id = film.Id }, film);
        }

        // DELETE: api/Films/5
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            if (_context.Films == null)
            {
                return NotFound();
            }
            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            _context.Films.Remove(film);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmExists(int id)
        {
            return (_context.Films?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
