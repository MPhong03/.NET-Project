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
using Microsoft.AspNetCore.Authorization;

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
                .Include(movie => movie.FilmGenres)
                .ThenInclude(filmGenre => filmGenre.Genre)
                .Include(movie => movie.Nation)
                .ToListAsync();

            var movieDtos = new List<MovieDto>();

            foreach (var movieEntity in movieEntities)
            {
                var movieDto = new MovieDto()
                {
                    Id = movieEntity.Id,
                    Time = movieEntity.Time,
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
                    isActiveBanner = movieEntity.isActiveBanner,
                    UploadDate = movieEntity.UploadDate,
                    Nation = new NationDto()
                    {
                        Id = movieEntity.Nation.Id,
                        Name = movieEntity.Nation.Name,
                    }
                };

                foreach (var filmCast in movieEntity.FilmCasts)
                {
                    var filmCastDto = new FilmCastDto()
                    {
                        Film = new FilmDto()
                        {
                            Name = filmCast.Film.Name,
                        },
                        Cast = new CastDto()
                        {
                            Name = filmCast.Cast.Name,
                            AvatarUrl = filmCast.Cast.AvatarUrl,
                            Id = filmCast.Cast.Id
                        },
                        Role = filmCast.Role
                    };
                    movieDto.FilmCasts.Add(filmCastDto);
                }

                foreach (var filmDirector in movieEntity.FilmDirectors)
                {
                    var filmDirectorDto = new FilmDirectorDto()
                    {
                        Director = new DirectorDto()
                        {
                            Name = filmDirector.Director.Name,
                            Id = filmDirector.Director.Id
                        }
                    };
                    movieDto.FilmDirectors.Add(filmDirectorDto);
                }

                foreach (var filmGenre in movieEntity.FilmGenres)
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
                    movieDto.FilmGenres.Add(filmGenreDto);
                }

                movieDtos.Add(movieDto);
            }

            return movieDtos;
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            if (_context.Movies == null)
            {
                return NotFound();
            }
            var movie = await _context.Movies
                .Include(m => m.FilmCasts)
                .ThenInclude(filmCast => filmCast.Cast)
                .Include(m => m.FilmDirectors)
                .ThenInclude(filmDirector => filmDirector.Director)
                .Include(m => m.FilmGenres)
                .ThenInclude(filmGenre => filmGenre.Genre)
                .Include(m => m.Nation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = new MovieDto()
            {
                Id = movie.Id,
                Time = movie.Time,
                Link = movie.Link,
                Name = movie.Name,
                ReleaseYear = movie.ReleaseYear,
                Description = movie.Description,
                IMDBScore = movie.IMDBScore,
                View = movie.View,
                PosterUrl = movie.PosterUrl,
                BackgroundUrl = movie.BackgroundUrl,
                LogoUrl = movie.LogoUrl,
                TrailerUrl = movie.TrailerUrl,
                isActiveBanner = movie.isActiveBanner,
                UploadDate = movie.UploadDate,
                Nation = new NationDto()
                {
                    Id = movie.Nation.Id,
                    Name = movie.Nation.Name,
                }
            };

            foreach (var filmCast in movie.FilmCasts)
            {
                var filmCastDto = new FilmCastDto()
                {
                    Film = new FilmDto()
                    {
                        Name = filmCast.Film.Name
                    },
                    Cast = new CastDto()
                    {
                        Name = filmCast.Cast.Name,
                        AvatarUrl = filmCast.Cast.AvatarUrl,
                        Id = filmCast.Cast.Id
                    },
                    Role = filmCast.Role
                };
                movieDto.FilmCasts.Add(filmCastDto);
            }

            foreach (var filmDirector in movie.FilmDirectors)
            {
                var filmDirectorDto = new FilmDirectorDto()
                {
                    Director = new DirectorDto()
                    {
                        Name = filmDirector.Director.Name,
                        Id = filmDirector.Director.Id
                    }
                };
                movieDto.FilmDirectors.Add(filmDirectorDto);
            }

            foreach (var filmGenre in movie.FilmGenres)
            {
                var filmGenreDto = new FilmGenreDto()
                {
                    Genre = new GenreDto()
                    {
                        Name = filmGenre.Genre.Name,
                        Id = filmGenre.Genre.Id
                    }
                };
                movieDto.FilmGenres.Add(filmGenreDto);
            }

            return movieDto;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieDto movieDto)
        {
            if (id != movieDto.Id)
            {
                return BadRequest();
            }

            var movie = await _context.Movies
                .Include(m => m.FilmCasts)
                .Include(m => m.FilmDirectors)
                .Include(m => m.FilmGenres)
                .Include(m => m.Nation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            movie.FilmCasts.Clear();
            movie.FilmDirectors.Clear();
            movie.FilmGenres.Clear();

            var nation = await _context.Nations.FindAsync(movie.Nation.Id);

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
            movie.isActiveBanner = movieDto.isActiveBanner;
            movie.UploadDate = movieDto.UploadDate;
            movie.Nation = nation;

            if (movieDto.FilmCasts != null)
            {
                foreach (var filmCastDto in movieDto.FilmCasts)
                {
                    var cast = await _context.Casts.FindAsync(filmCastDto.Cast.Id);
                    if (cast != null)
                    {
                        var filmCast = new FilmCast
                        {
                            Cast = cast,
                            Role = filmCastDto.Role
                        };
                        movie.FilmCasts.Add(filmCast);
                    }
                }
            }

            if (movieDto.FilmDirectors != null)
            {
                foreach (var filmDirectorDto in movieDto.FilmDirectors)
                {
                    var director = await _context.Directors.FindAsync(filmDirectorDto.Director.Id);
                    if (director != null)
                    {
                        var filmDirector = new FilmDirector
                        {
                            Director = director
                        };
                        movie.FilmDirectors.Add(filmDirector);
                    }
                }
            }

            if (movieDto.FilmGenres != null)
            {
                foreach (var filmGenreDto in movieDto.FilmGenres)
                {
                    var genre = await _context.Genres.FindAsync(filmGenreDto.Genre.Id);
                    if (genre != null)
                    {
                        var filmGenre = new FilmGenre
                        {
                            Genre = genre
                        };
                        movie.FilmGenres.Add(filmGenre);
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
        [Authorize(Roles = "ROLE_ADMIN")]
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
                TrailerUrl = movieDto.TrailerUrl,
                isActiveBanner = movieDto.isActiveBanner,
                UploadDate = movieDto.UploadDate,
                Nation = await _context.Nations.FindAsync(movieDto.Nation.Id)
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

            if (movieDto.FilmGenres != null)
            {
                foreach (var filmGenreDto in movieDto.FilmGenres)
                {

                    FilmGenre filmGenre = new FilmGenre
                    {
                        Genre = await _context.Genres.FindAsync(filmGenreDto.Genre.Id),
                    };

                    movie.FilmGenres.Add(filmGenre);
                }
            }

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }


        // DELETE: api/Movies/5
        [Authorize(Roles = "ROLE_ADMIN")]
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
