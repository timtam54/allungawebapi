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
    public class ReadingsController : ControllerBase
    {
        private readonly dbcontext _context;

        public ReadingsController(dbcontext context)
        {
            _context = context;
        }

        // GET: api/Readings
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Reading>>> GetReading(string id)
        {
            if (_context.Reading == null)
            {
                return NotFound();
            }
            var dd = await _context.Reading.Where(i => i.reportid == Convert.ToInt32(id)).ToListAsync();
            return dd;
        }

        // GET: api/Readings/5
        [HttpGet("int/{id}")]
        public async Task<ActionResult<Reading>> GetReading(int id)
        {
            if (_context.Reading == null)
            {
                return NotFound();
            }
            var reading = await _context.Reading.FindAsync(id);

            if (reading == null)
            {
                return NotFound();
            }

            return reading;
        }

        // PUT: api/Readings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReading(int id,List<Reading> readings)
        {
            var reading = readings.FirstOrDefault();
            var db = await _context.Reading.Where(i => i.reportid == Convert.ToInt32(id)).ToListAsync();
            foreach (var item in db)
            {
                var rd = readings.Where(i => i.Paramid == item.Paramid && i.sampleid == item.sampleid).FirstOrDefault();
                if (rd == null)
                    item.value = null;
                else
                    item.value = rd.value;
            }
            _context.SaveChanges();
            foreach (var item in readings)
            {
                var rd = db.Where(i => i.Paramid == item.Paramid && i.sampleid == item.sampleid).FirstOrDefault();
                if (rd == null)
                {
                    Reading newrd = new Reading();
                    newrd.value = item.value;
                    newrd.sampleid = item.sampleid;
                    newrd.Paramid = item.Paramid;
                    newrd.reportid = item.reportid;
                    _context.Reading.Add(newrd);
                    _context.SaveChanges();
                }
            }
 

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReadingExists(id))
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

        // POST: api/Readings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reading>> PostReading(Reading reading)
        {
            if (_context.Reading == null)
            {
                return Problem("Entity set 'dbcontext.Reading'  is null.");
            }
            _context.Reading.Add(reading);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReading", new { id = reading.Readingid }, reading);
        }

        // DELETE: api/Readings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReading(int id)
        {
            if (_context.Reading == null)
            {
                return NotFound();
            }
            var reading = await _context.Reading.FindAsync(id);
            if (reading == null)
            {
                return NotFound();
            }

            _context.Reading.Remove(reading);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReadingExists(int id)
        {
            return (_context.Reading?.Any(e => e.Readingid == id)).GetValueOrDefault();
        }
    }
}
