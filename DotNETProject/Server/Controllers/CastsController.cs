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

            List<Cast> list = await _context.Casts.ToListAsync();
            List<CastDto> result = new List<CastDto>();

            foreach (var item in list)
            {
                CastDto itemResult = new CastDto();
                itemResult.Id = item.Id;
                itemResult.Name = item.Name;
                itemResult.Description = item.Description;
                itemResult.BirthDate = item.BirthDate;
                itemResult.AvatarUrl = item.AvatarUrl;
                itemResult.Gender = item.Gender;
                itemResult.Nation = item.Nation;
                result.Add(itemResult);
            }

            return result;
        }

        // GET: api/Casts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cast>> GetCast(int id)
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

            return cast;
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
