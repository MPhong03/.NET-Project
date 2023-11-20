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
    public class EpisodesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EpisodesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Episodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Episode>>> GetEpisodes()
        {
            if (_context.Episodes == null)
            {
                return NotFound();
            }
            return await _context.Episodes.ToListAsync();
        }

        // GET: api/Episodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EpisodeDto>> GetEpisode(int id)
        {
            if (_context.Episodes == null)
            {
                return NotFound();
            }
            var episode = await _context.Episodes
                        .Include(e => e.Series)
                        .FirstOrDefaultAsync(e => e.Id == id);
            if (episode == null)
            {
                return NotFound();
            }

            EpisodeDto episodeDto = new EpisodeDto();
            episodeDto.Id = id;
            episodeDto.EpisodeNumber = episode.EpisodeNumber;
            episodeDto.EpisodeName = episode.EpisodeName;
            episodeDto.Link = episode.Link;
            episodeDto.Time = episode.Time;
            episodeDto.View = episode.View;
            episodeDto.Series.Name = episode.Series.Name;

            

            return episodeDto;
        }

        // PUT: api/Episodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEpisode(int id, EpisodeDto episodeDto)
        {
            if (id != episodeDto.Id)
            {
                return BadRequest();
            }

            var episode = await _context.Episodes.FindAsync(id);

            if (episode == null)
            {
                return NotFound();
            }

            episode.EpisodeNumber = episodeDto.EpisodeNumber;
            episode.EpisodeName = episodeDto.EpisodeName;
            episode.Link = episodeDto.Link;
            episode.Time = episodeDto.Time;
            episode.View = episodeDto.View;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EpisodeExists(id))
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

        [HttpPut("view/{id}")]
        public async Task<IActionResult> PutEpisodeView(int id, EpisodeDto episodeDto)
        {
            if (id != episodeDto.Id)
            {
                return BadRequest();
            }

            var episode = await _context.Episodes.FindAsync(id);

            if (episode == null)
            {
                return NotFound();
            }

            episode.View = episodeDto.View;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EpisodeExists(id))
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

        // POST: api/Episodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPost]
        public async Task<ActionResult<EpisodeDto>> PostEpisode(EpisodeDto episodeDto)
        {
            var episode = new Episode
            {
                EpisodeNumber = episodeDto.EpisodeNumber,
                EpisodeName = episodeDto.EpisodeName,
                Link = episodeDto.Link,
                Time = episodeDto.Time,
                View = episodeDto.View,
                Series = await _context.TVSeries.FindAsync(episodeDto.Series.Id)
            };

            _context.Episodes.Add(episode);
            await _context.SaveChangesAsync();

            var createdEpisodeDto = new EpisodeDto
            {
                Id = episode.Id,
                EpisodeNumber = episode.EpisodeNumber,
                EpisodeName = episode.EpisodeName,
                Link = episode.Link,
                Time = episode.Time,
                View = episode.View,
                Series = episodeDto.Series
            };

            return CreatedAtAction("GetEpisode", new { id = createdEpisodeDto.Id }, createdEpisodeDto);
        }

        // DELETE: api/Episodes/5
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEpisode(int id)
        {
            if (_context.Episodes == null)
            {
                return NotFound();
            }
            var episode = await _context.Episodes.FindAsync(id);
            if (episode == null)
            {
                return NotFound();
            }

            _context.Episodes.Remove(episode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EpisodeExists(int id)
        {
            return (_context.Episodes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
