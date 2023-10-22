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
    public class TVSeriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TVSeriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TVSeries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TVSeriesDto>>> GetTVSeries()
        {
            if (_context.TVSeries == null)
            {
                return NotFound();
            }

            var tVSeriesEntities = await _context.TVSeries
                .Include(tVSeries => tVSeries.FilmCasts)
                .ThenInclude(filmCast => filmCast.Cast)
                .Include(tVSeries => tVSeries.FilmDirectors)
                .ThenInclude(filmDirector => filmDirector.Director)
                .Include(tVSeries => tVSeries.episodes)
                .ToListAsync();

            var TVSeriesDtos = new List<TVSeriesDto>();

            foreach (var tVSeriesEntity in tVSeriesEntities)
            {
                var TVSeriesDto = new TVSeriesDto()
                {
                    Id = tVSeriesEntity.Id,
                    Name = tVSeriesEntity.Name,
                    ReleaseYear = tVSeriesEntity.ReleaseYear,
                    Description = tVSeriesEntity.Description,
                    IMDBScore = tVSeriesEntity.IMDBScore,
                    View = tVSeriesEntity.View,
                    PosterUrl = tVSeriesEntity.PosterUrl,
                    BackgroundUrl = tVSeriesEntity.BackgroundUrl,
                    LogoUrl = tVSeriesEntity.LogoUrl,
                    TrailerUrl = tVSeriesEntity.TrailerUrl,
                };

                foreach (var filmCast in tVSeriesEntity.FilmCasts)
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
                    TVSeriesDto.FilmCasts.Add(filmCastDto);
                }

                foreach (var filmDirector in tVSeriesEntity.FilmDirectors)
                {
                    var filmDirectorDto = new FilmDirectorDto()
                    {
                        DirectorId = filmDirector.DirectorId,
                        Director = new DirectorDto()
                        {
                            Name = filmDirector.Director.Name
                        }
                    };
                    TVSeriesDto.FilmDirectors.Add(filmDirectorDto);
                }

                foreach (var episode in tVSeriesEntity.episodes)
                {
                    var episodeDto = new EpisodeDto()
                    {
                        Id = episode.Id,
                        EpisodeNumber = episode.EpisodeNumber,
                        EpisodeName = episode.EpisodeName,
                        Link = episode.Link,
                        Time = episode.Time,
                        View = episode.View
                    };
                    TVSeriesDto.episodes.Add(episodeDto);
                }

                TVSeriesDtos.Add(TVSeriesDto);
            }

            return TVSeriesDtos;
        }

        // GET: api/TVSeries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TVSeriesDto>> GetTVSeries(int id)
        {
            if (_context.TVSeries == null)
            {
                return NotFound();
            }

            var tVSeriesEntity = await _context.TVSeries
                .Include(tVSeries => tVSeries.FilmCasts)
                .ThenInclude(filmCast => filmCast.Cast)
                .Include(tVSeries => tVSeries.FilmDirectors)
                .ThenInclude(filmDirector => filmDirector.Director)
                .Include(tVSeries => tVSeries.episodes)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tVSeriesEntity == null)
            {
                return NotFound();
            }

            var tVSeriesDto = new TVSeriesDto()
            {
                Id = tVSeriesEntity.Id,
                Name = tVSeriesEntity.Name,
                ReleaseYear = tVSeriesEntity.ReleaseYear,
                Description = tVSeriesEntity.Description,
                IMDBScore = tVSeriesEntity.IMDBScore,
                View = tVSeriesEntity.View,
                PosterUrl = tVSeriesEntity.PosterUrl,
                BackgroundUrl = tVSeriesEntity.BackgroundUrl,
                LogoUrl = tVSeriesEntity.LogoUrl,
                TrailerUrl = tVSeriesEntity.TrailerUrl,
            };

            foreach (var filmCast in tVSeriesEntity.FilmCasts)
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
                tVSeriesDto.FilmCasts.Add(filmCastDto);
            }

            foreach (var filmDirector in tVSeriesEntity.FilmDirectors)
            {
                var filmDirectorDto = new FilmDirectorDto()
                {
                    DirectorId = filmDirector.DirectorId,
                    Director = new DirectorDto()
                    {
                        Name = filmDirector.Director.Name
                    }
                };
                tVSeriesDto.FilmDirectors.Add(filmDirectorDto);
            }

            foreach (var episode in tVSeriesEntity.episodes)
            {
                var episodeDto = new EpisodeDto()
                {
                    Id = episode.Id,
                    EpisodeNumber = episode.EpisodeNumber,
                    EpisodeName = episode.EpisodeName,
                    Link = episode.Link,
                    Time = episode.Time,
                    View = episode.View
                };
                tVSeriesDto.episodes.Add(episodeDto);
            }

            return tVSeriesDto;
        }

        // PUT: api/TVSeries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTVSeries(int id, TVSeriesDto tVSeriesDto)
        {
            if (id != tVSeriesDto.Id)
            {
                return BadRequest();
            }

            var tVSeries = await _context.TVSeries
                .Include(m => m.FilmCasts)
                .Include(m => m.FilmDirectors)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tVSeries == null)
            {
                return NotFound();
            }

            tVSeries.FilmCasts.Clear();
            tVSeries.FilmDirectors.Clear();

            tVSeries.Name = tVSeriesDto.Name;
            tVSeries.ReleaseYear = tVSeriesDto.ReleaseYear;
            tVSeries.Description = tVSeriesDto.Description;
            tVSeries.IMDBScore = tVSeriesDto.IMDBScore;
            tVSeries.View = tVSeriesDto.View;
            tVSeries.PosterUrl = tVSeriesDto.PosterUrl;
            tVSeries.BackgroundUrl = tVSeriesDto.BackgroundUrl;
            tVSeries.LogoUrl = tVSeriesDto.LogoUrl;
            tVSeries.TrailerUrl = tVSeriesDto.TrailerUrl;

            if (tVSeriesDto.FilmCasts != null)
            {
                foreach (var filmCastDto in tVSeriesDto.FilmCasts)
                {
                    var cast = await _context.Casts.FindAsync(filmCastDto.Cast.Id);
                    if (cast != null)
                    {
                        var filmCast = new FilmCast
                        {
                            Cast = cast,
                            Role = filmCastDto.Role
                        };
                        tVSeries.FilmCasts.Add(filmCast);
                    }
                }
            }

            if (tVSeriesDto.FilmDirectors != null)
            {
                foreach (var filmDirectorDto in tVSeriesDto.FilmDirectors)
                {
                    var director = await _context.Directors.FindAsync(filmDirectorDto.Director.Id);
                    if (director != null)
                    {
                        var filmDirector = new FilmDirector
                        {
                            Director = director
                        };
                        tVSeries.FilmDirectors.Add(filmDirector);
                    }
                }
            }

            if (tVSeriesDto.episodes != null)
            {
                tVSeries.episodes.Clear();

                foreach (var itemDto in tVSeriesDto.episodes)
                {
                    var episode = new Episode
                    {
                        Id = itemDto.Id,
                        EpisodeNumber = itemDto.EpisodeNumber,
                        EpisodeName = itemDto.EpisodeName,
                        Link = itemDto.Link,
                        Time = itemDto.Time,
                        View = itemDto.View
                    };
                    tVSeries.episodes.Add(episode);
                }
            }


            _context.Entry(tVSeries).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TVSeriesExists(id))
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

        // POST: api/TVSeries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TVSeries>> PostTVSeries(TVSeriesDto tVSeriesDto)
        {
            if (_context.TVSeries == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TVSeries'  is null.");
            }

            if (tVSeriesDto.FilmCasts == null)
            {
                return Problem("FilmCasts collection is null.");
            }

            TVSeries tVSeries = new TVSeries
            {
                Name = tVSeriesDto.Name,
                ReleaseYear = tVSeriesDto.ReleaseYear,
                Description = tVSeriesDto.Description,
                IMDBScore = tVSeriesDto.IMDBScore,
                View = tVSeriesDto.View,
                PosterUrl = tVSeriesDto.PosterUrl,
                BackgroundUrl = tVSeriesDto.BackgroundUrl,
                LogoUrl = tVSeriesDto.LogoUrl,
                TrailerUrl = tVSeriesDto.TrailerUrl
            };

            if (tVSeriesDto.FilmCasts != null)
            {
                foreach (var filmCastDto in tVSeriesDto.FilmCasts)
                {
                    FilmCast filmCast = new FilmCast
                    {
                        Cast = await _context.Casts.FindAsync(filmCastDto.Cast.Id),
                        Role = filmCastDto.Role
                    };

                    tVSeries.FilmCasts.Add(filmCast);
                }
            }

            if (tVSeriesDto.FilmDirectors != null)
            {
                foreach (var filmDirectorDto in tVSeriesDto.FilmDirectors)
                {
                    FilmDirector filmDirector = new FilmDirector
                    {
                        Director = await _context.Directors.FindAsync(filmDirectorDto.Director.Id),
                    };

                    tVSeries.FilmDirectors.Add(filmDirector);
                }
            }

            if (tVSeriesDto.episodes != null)
            {
                foreach (var itemDto in tVSeriesDto.episodes)
                {
                    var episode = new Episode
                    {
                        Id = itemDto.Id,
                        EpisodeNumber = itemDto.EpisodeNumber,
                        EpisodeName = itemDto.EpisodeName,
                        Link = itemDto.Link,
                        Time = itemDto.Time,
                        View = itemDto.View
                    };
                    tVSeries.episodes.Add(episode);
                }
            }

            _context.TVSeries.Add(tVSeries);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTVSeries", new { id = tVSeries.Id }, tVSeries);
        }

        // DELETE: api/TVSeries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTVSeries(int id)
        {
            if (_context.TVSeries == null)
            {
                return NotFound();
            }
            var tVSeries = await _context.TVSeries.FindAsync(id);
            if (tVSeries == null)
            {
                return NotFound();
            }

            _context.TVSeries.Remove(tVSeries);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TVSeriesExists(int id)
        {
            return (_context.TVSeries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
