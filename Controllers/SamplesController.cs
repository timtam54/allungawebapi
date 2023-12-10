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
   // [Authorize]
    //[RequiredScope("tasks.read")]
    [Route("api/[controller]")]
    [ApiController]
    public class SamplesController : ControllerBase
    {
        private readonly dbcontext _context;

        public SamplesController(dbcontext context)
        {
            _context = context;
        }
        [HttpGet("report/{id:int}")]
       // [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SampleSearch>>> GetByReport(int id)
        {
            
            if (_context.Sample == null)
            {
                return NotFound();
            }
            var sams = await (from rd in _context.Reading where rd.reportid == id select rd.sampleid).ToListAsync();
 
            return await (from sm in _context.Sample join et in _context.ExposureType on sm.ExposureTypeID equals et.ExposureTypeID
                           where sams.Contains( sm.SampleID)
                          select new SampleSearch { SampleOrder = sm.SampleOrder, EquivalentSamples = sm.EquivalentSamples, description = sm.description, ExposureType = et.Name, longdescription = sm.longdescription, Number = sm.Number, Reportable = sm.Reportable, SampleID = sm.SampleID, seriesid = sm.seriesid }).ToListAsync();
            //                           join rd in _context.Reading on sm.SampleID equals rd.sampleid where rd.reportid == id select new SampleSearch { SampleOrder = sm.SampleOrder, EquivalentSamples = sm.EquivalentSamples, description = sm.description, ExposureType = et.Name, longdescription = sm.longdescription, Number = sm.Number, Reportable = sm.Reportable, SampleID = sm.SampleID, seriesid = sm.seriesid }).ToListAsync();
        }
        // GET: api/Samples
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SampleSearch>>> GetSample(string id)
        {
            string[] ss = id.Split(new char[] { char.Parse("~") }, StringSplitOptions.RemoveEmptyEntries);
            int seriesid =Convert.ToInt32( ss[0]);
            int delint = Convert.ToInt32(ss[1]);
            bool del = (delint == 1);
            if (_context.Sample == null)
            {
                return NotFound();
            }
            if (del)
                return await (from sm in _context.Sample join et in _context.ExposureType on sm.ExposureTypeID equals et.ExposureTypeID where sm.seriesid == seriesid && sm.Deleted == del select new SampleSearch { SampleOrder = sm.SampleOrder, EquivalentSamples = sm.EquivalentSamples, description = sm.description, ExposureType = et.Name, longdescription = sm.longdescription, Number = sm.Number, Reportable = sm.Reportable, SampleID = sm.SampleID, seriesid = sm.seriesid }).ToListAsync();

            return await (from sm in _context.Sample join et in _context.ExposureType on sm.ExposureTypeID equals et.ExposureTypeID where sm.seriesid == seriesid && (sm.Deleted== del || sm.Deleted==null) select new SampleSearch { SampleOrder=sm.SampleOrder, EquivalentSamples=sm.EquivalentSamples, description = sm.description, ExposureType = et.Name,  longdescription = sm.longdescription, Number = sm.Number, Reportable = sm.Reportable, SampleID = sm.SampleID, seriesid = sm.seriesid }).ToListAsync();
        }

        // GET: api/Samples/5

        [HttpGet("int/{id:int}")]
        public async Task<ActionResult<Sample>> GetSample(int id)
        {
            if (id == 0)
            {
                return new Sample();
            }
            if (_context.Sample == null)
            {
                return NotFound();
            }
            var sample = await _context.Sample.FindAsync(id);

            if (sample == null)
            {
                return NotFound();
            }

            return sample;
        }

        // PUT: api/Samples/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSample(int id, Sample sample)
        {
            if (id != sample.SampleID)
            {
                return BadRequest();
            }

            _context.Entry(sample).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SampleExists(id))
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

        // POST: api/Samples
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sample>> PostSample(Sample sample)
        {
            if (_context.Sample == null)
            {
                return Problem("Entity set 'dbcontext.Sample'  is null.");
            }
            _context.Sample.Add(sample);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSample", new { id = sample.SampleID }, sample);
        }

        // DELETE: api/Samples/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSample(int id)
        {
            //probably need to check if
            if (_context.Sample == null)
            {
                return NotFound();
            }
            var sample = await _context.Sample.FindAsync(id);
            if (sample == null)
            {
                return NotFound();
            }

            _context.Sample.Remove(sample);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SampleExists(int id)
        {
            return (_context.Sample?.Any(e => e.SampleID == id)).GetValueOrDefault();
        }
    }
}
