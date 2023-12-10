using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AllungaWebAPI.Data;
using AllungaWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace AllungaWebAPI.Controllers
{
    [Authorize]
    [RequiredScope("tasks.read")]
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesProjectedReturnsController : ControllerBase
    {
        private readonly dbcontext _context;

        public SeriesProjectedReturnsController(dbcontext context)
        {
            _context = context;
        }

        // GET: api/SeriesProjectedReturns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeriesProjectedReturns>>> GetSeriesProjectedReturns()
        {
          if (_context.SeriesProjectedReturns == null)
          {
              return NotFound();
          }
            return await _context.SeriesProjectedReturns.ToListAsync();
        }

        // GET: api/SeriesProjectedReturns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SeriesProjectedReturns>> GetSeriesProjectedReturns(int id)
        {
          if (_context.SeriesProjectedReturns == null)
          {
              return NotFound();
          }
            var seriesProjectedReturns = await _context.SeriesProjectedReturns.FindAsync(id);

            if (seriesProjectedReturns == null)
            {
                return NotFound();
            }

            return seriesProjectedReturns;
        }

        // PUT: api/SeriesProjectedReturns/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeriesProjectedReturns(int id, SeriesProjectedReturns seriesProjectedReturns)
        {
            if (id != seriesProjectedReturns.id)
            {
                return BadRequest();
            }

            _context.Entry(seriesProjectedReturns).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeriesProjectedReturnsExists(id))
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

        // POST: api/SeriesProjectedReturns
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SeriesProjectedReturns>> PostSeriesProjectedReturns(SeriesProjectedReturns seriesProjectedReturns)
        {
          if (_context.SeriesProjectedReturns == null)
          {
              return Problem("Entity set 'dbcontext.SeriesProjectedReturns'  is null.");
          }
            _context.SeriesProjectedReturns.Add(seriesProjectedReturns);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeriesProjectedReturns", new { id = seriesProjectedReturns.id }, seriesProjectedReturns);
        }

        // DELETE: api/SeriesProjectedReturns/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeriesProjectedReturns(int id)
        {
            if (_context.SeriesProjectedReturns == null)
            {
                return NotFound();
            }
            var seriesProjectedReturns = await _context.SeriesProjectedReturns.FindAsync(id);
            if (seriesProjectedReturns == null)
            {
                return NotFound();
            }

            _context.SeriesProjectedReturns.Remove(seriesProjectedReturns);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeriesProjectedReturnsExists(int id)
        {
            return (_context.SeriesProjectedReturns?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
