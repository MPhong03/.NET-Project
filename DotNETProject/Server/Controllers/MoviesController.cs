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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DotNETProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }

            var movieEntities = await _context.Movies
                .Include(movie => movie.FilmCasts)
                .ThenInclude(filmCast => filmCast.Cast)
                .Include(movie => movie.FilmDirectors)
                .ThenInclude(filmDirector => filmDirector.Director)
                .ToListAsync();

            var movieDtos = new List<MovieDto>();

            foreach (var movieEntity in movieEntities)
            {
                var movieDto = new MovieDto()
                {
                    Id = movieEntity.Id,
                    Time = movieEntity.Time,
                    Title = movieEntity.Title,
                    Link = movieEntity.Link,
                    Name = movieEntity.Name,
                    ReleaseYear = movieEntity.ReleaseYear,
                    Description = movieEntity.Description,
                    IMDBScore = movieEntity.IMDBScore,
                    View = movieEntity.View,
                    PosterUrl = movieEntity.PosterUrl,
                    BackgroundUrl = movieEntity.BackgroundUrl,
                    LogoUrl = movieEntity.LogoUrl,
                    TrailerUrl = movieEntity.TrailerUrl,
                };

                foreach (var filmCast in movieEntity.FilmCasts)
                {
                    var filmCastDto = new FilmCastDto()
                    {
                        Film = new FilmDto()
                        {
                            Name = filmCast.Film.Name
                        },
                        Cast = new CastDto()
                        {
                            Name = filmCast.Cast.Name
                        },
                        Role = filmCast.Role
                    };
                    movieDto.FilmCasts.Add(filmCastDto);
                }

                foreach (var filmDirector in movieEntity.FilmDirectors)
                {
                    var filmDirectorDto = new FilmDirectorDto()
                    {
                        DirectorId = filmDirector.DirectorId,
                        Director = new DirectorDto()
                        {
                            Name = filmDirector.Director.Name
                        }
                    };
                    movieDto.FilmDirectors.Add(filmDirectorDto);
                }

                movieDtos.Add(movieDto);
            }

            return movieDtos;
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieDto movieDto)
        {
            if (id != movieDto.Id)
            {
                return BadRequest();
            }

            // Retrieve the movie from the database by Id
            var movie = await _context.Movies
                .Include(m => m.FilmCasts)
                .Include(m => m.FilmDirectors)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            // Clear the existing FilmCasts and FilmDirectors for this movie
            movie.FilmCasts.Clear();
            movie.FilmDirectors.Clear();

            // Update the movie entity with the data from movieDto
            movie.Title = movieDto.Title;
            movie.Time = movieDto.Time;
            movie.Link = movieDto.Link;
            movie.Name = movieDto.Name;
            movie.ReleaseYear = movieDto.ReleaseYear;
            movie.Description = movieDto.Description;
            movie.IMDBScore = movieDto.IMDBScore;
            movie.View = movieDto.View;
            movie.PosterUrl = movieDto.PosterUrl;
            movie.BackgroundUrl = movieDto.BackgroundUrl;
            movie.LogoUrl = movieDto.LogoUrl;
            movie.TrailerUrl = movieDto.TrailerUrl;

            // Update the FilmCasts relationships
            if (movieDto.FilmCasts != null)
            {
                foreach (var filmCastDto in movieDto.FilmCasts)
                {
                    // Retrieve the corresponding Cast entity from the database by Id
                    var cast = await _context.Casts.FindAsync(filmCastDto.Cast.Id);
                    if (cast != null)
                    {
                        // Create a new FilmCast and associate it with the Cast
                        var filmCast = new FilmCast
                        {
                            Cast = cast,
                            Role = filmCastDto.Role
                        };
                        movie.FilmCasts.Add(filmCast);
                    }
                }
            }

            // Update the FilmDirectors relationships
            if (movieDto.FilmDirectors != null)
            {
                foreach (var filmDirectorDto in movieDto.FilmDirectors)
                {
                    // Retrieve the corresponding Director entity from the database by Id
                    var director = await _context.Directors.FindAsync(filmDirectorDto.Director.Id);
                    if (director != null)
                    {
                        // Create a new FilmDirector and associate it with the Director
                        var filmDirector = new FilmDirector
                        {
                            Director = director
                        };
                        movie.FilmDirectors.Add(filmDirector);
                    }
                }
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieDto movieDto)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Movies' is null.");
            }

            if (movieDto.FilmCasts == null)
            {
                return Problem("FilmCasts collection is null.");
            }

            Movie movie = new Movie
            {
                Title = movieDto.Title,
                Time = movieDto.Time,
                Link = movieDto.Link,
                Name = movieDto.Name,
                ReleaseYear = movieDto.ReleaseYear,
                Description = movieDto.Description,
                IMDBScore = movieDto.IMDBScore,
                View = movieDto.View,
                PosterUrl = movieDto.PosterUrl,
                BackgroundUrl = movieDto.BackgroundUrl,
                LogoUrl = movieDto.LogoUrl,
                TrailerUrl = movieDto.TrailerUrl
            };

            if (movieDto.FilmCasts != null)
            {
                foreach (var filmCastDto in movieDto.FilmCasts)
                {
                    //Cast cast = new Cast
                    //{
                    //    Name = filmCastDto.Cast.Name,
                    //    AvatarUrl = filmCastDto.Cast.AvatarUrl,
                    //    BirthDate = filmCastDto.Cast.BirthDate,
                    //    Gender = filmCastDto.Cast.Gender,
                    //    Nation = filmCastDto.Cast.Nation,
                    //    Description = filmCastDto.Cast.Description
                    //};

                    FilmCast filmCast = new FilmCast
                    {
                        Cast = await _context.Casts.FindAsync(filmCastDto.Cast.Id),
                        Role = filmCastDto.Role
                    };

                    movie.FilmCasts.Add(filmCast);
                }
            }

            if (movieDto.FilmDirectors != null)
            {
                foreach (var filmDirectorDto in movieDto.FilmDirectors)
                {
                    //Director director = new Director
                    //{
                    //    Name = filmDirectorDto.Director.Name,
                    //    AvatarUrl = filmDirectorDto.Director.AvatarUrl,
                    //    BirthDate = filmDirectorDto.Director.BirthDate,
                    //    Gender = filmDirectorDto.Director.Gender,
                    //    Nation = filmDirectorDto.Director.Nation,
                    //    Description = filmDirectorDto.Director.Description
                    //};

                    FilmDirector filmDirector = new FilmDirector
                    {
                        Director = await _context.Directors.FindAsync(filmDirectorDto.Director.Id),
                    };

                    movie.FilmDirectors.Add(filmDirector);
                }
            }

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }


        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
