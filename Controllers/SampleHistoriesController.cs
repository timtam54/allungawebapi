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
    public class SampleHistoriesController : ControllerBase
    {
        private readonly dbcontext _context;

        public SampleHistoriesController(dbcontext context)
        {
            _context = context;
        }

        // GET: api/SampleHistories
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SampleHistory>>> GetSampleHistory(string id)
        {
          if (_context.SampleHistory == null)
          {
              return NotFound();
          }
            return await _context.SampleHistory.Where(i=>i.SampleID==Convert.ToInt32(id)).OrderBy(i=>i.DTE).ToListAsync();
        }

        // GET: api/SampleHistories/5
        [HttpGet("int/{id}")]
        public async Task<ActionResult<SampleHistory>> GetSampleHistory(int id)
        {
          if (_context.SampleHistory == null)
          {
              return NotFound();
          }
            var sampleHistory = await _context.SampleHistory.FindAsync(id);

            if (sampleHistory == null)
            {
                return NotFound();
            }

            return sampleHistory;
        }

        // PUT: api/SampleHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSampleHistory(int id, SampleHistory sampleHistory)
        {
            if (id != sampleHistory.SampleHistoryID)
            {
                return BadRequest();
            }

            _context.Entry(sampleHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SampleHistoryExists(id))
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

        // POST: api/SampleHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SampleHistory>> PostSampleHistory(SampleHistory sampleHistory)
        {
          if (_context.SampleHistory == null)
          {
              return Problem("Entity set 'dbcontext.SampleHistory'  is null.");
          }
            _context.SampleHistory.Add(sampleHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSampleHistory", new { id = sampleHistory.SampleHistoryID }, sampleHistory);
        }

        // DELETE: api/SampleHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSampleHistory(int id)
        {
            if (_context.SampleHistory == null)
            {
                return NotFound();
            }
            var sampleHistory = await _context.SampleHistory.FindAsync(id);
            if (sampleHistory == null)
            {
                return NotFound();
            }

            _context.SampleHistory.Remove(sampleHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SampleHistoryExists(int id)
        {
            return (_context.SampleHistory?.Any(e => e.SampleHistoryID == id)).GetValueOrDefault();
        }
    }
}
