using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNETProject.Server.Data;
using DotNETProject.Server.Models;

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
        public async Task<ActionResult<IEnumerable<TVSeries>>> GetTVSeries()
        {
          if (_context.TVSeries == null)
          {
              return NotFound();
          }
            return await _context.TVSeries.ToListAsync();
        }

        // GET: api/TVSeries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TVSeries>> GetTVSeries(int id)
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

            return tVSeries;
        }

        // PUT: api/TVSeries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTVSeries(int id, TVSeries tVSeries)
        {
            if (id != tVSeries.Id)
            {
                return BadRequest();
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
        public async Task<ActionResult<TVSeries>> PostTVSeries(TVSeries tVSeries)
        {
          if (_context.TVSeries == null)
          {
              return Problem("Entity set 'ApplicationDbContext.TVSeries'  is null.");
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
