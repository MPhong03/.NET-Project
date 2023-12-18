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
    public class NationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Nations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NationDto>>> GetNations()
        {
            if (_context.Nations == null)
            {
                return NotFound();
            }

            var nations = await _context.Nations.ToListAsync();
            var list = new List<NationDto>();

            foreach (var nation in nations)
            {
                var n = new NationDto() { Id = nation.Id, Name = nation.Name };
                list.Add(n);
            }
            return list;
        }

        // GET: api/Nations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NationDto>> GetNation(int id)
        {
            if (_context.Nations == null)
            {
                return NotFound();
            }
            var nation = await _context.Nations.FindAsync(id);

            if (nation == null)
            {
                return NotFound();
            }

            return new NationDto() { Id = nation.Id, Name = nation.Name };
        }

        // PUT: api/Nations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNation(int id, NationDto nationDto)
        {
            if (id != nationDto.Id)
            {
                return BadRequest();
            }

            var nation = _context.Nations.Find(id);

            nation.Name = nationDto.Name;

            _context.Entry(nation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NationExists(id))
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

        // POST: api/Nations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPost]
        public async Task<ActionResult<Nation>> PostNation(NationDto nationDto)
        {
            if (_context.Nations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Nations'  is null.");
            }

            var nation = new Nation()
            {
                Name = nationDto.Name
            };

            _context.Nations.Add(nation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNation", new { id = nation.Id }, nation);
        }

        // DELETE: api/Nations/5
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNation(int id)
        {
            if (_context.Nations == null)
            {
                return NotFound();
            }
            var nation = await _context.Nations.FindAsync(id);
            if (nation == null)
            {
                return NotFound();
            }

            _context.Nations.Remove(nation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NationExists(int id)
        {
            return (_context.Nations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
