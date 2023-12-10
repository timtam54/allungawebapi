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
    public class SeriesNextEventController : ControllerBase
    {
        private readonly dbcontext _context;

        public SeriesNextEventController(dbcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeriesNextEvent>>> GetNextEvent(DateTime frm, DateTime to)
        {
            if (_context.Series == null)
            {
                return NotFound();
            }
            //from  = DateTime.Now.AddDays(-1200)
            //to = DateTime.Now.AddDays(900)
            var ReportNext =  await (from srs in _context.Series join cl in _context.Client on srs.clientid equals cl.clientid where (srs.NextProjectedReportDate> frm) && (srs.NextProjectedReportDate<to) select new SeriesNextEvent { EventType="Report", NextEventDate=srs.NextProjectedReportDate.Value, seriesid=srs.seriesid, NextEventDesc= srs.NextProjectedReportCalc, NextEventName=srs.NextProjectedReportName , ClientID = cl.clientid, AllungaRef = srs.AllungaReference, ClientName = cl.companyname } ).ToListAsync();
            var ReturnNext = await (from srs in _context.SeriesProjectedReturns join sr in _context.Series on srs.seriesid equals sr.seriesid join cl in _context.Client on sr.clientid equals cl.clientid where srs.ReturnDate > frm && srs.ReturnDate < to select new SeriesNextEvent { ClientID=cl.clientid, AllungaRef=sr.AllungaReference, ClientName=cl.companyname, EventType = "Return", NextEventDate = srs.ReturnDate, seriesid = srs.seriesid, NextEventDesc = srs.SeriesProjectedReturnCalc, NextEventName = srs.ReturnName }).ToListAsync();
            var bb = ReportNext.Concat(ReturnNext);
            return bb.OrderBy(i=>i.NextEventDate).ToList();
        }

        // GET: api/Clients
        /*     [HttpGet("{id}")]
             public async Task<ActionResult<IEnumerable<SeriesSearch>>> GetSeries(string id)
             {
                 if (_context.Series == null)
                 {
                     return NotFound();
                 }
                 var xx = await (from sr in _context.Series join cl in _context.Client on sr.clientid equals cl.clientid join et in _context.ExposureType on sr.ExposureTypeID equals et.ExposureTypeID where sr.AllungaReference!.ToLower().Contains(id.ToLower()) || sr.ShortDescription!.ToLower().Contains(id.ToLower()) || cl.companyname!.ToLower().Contains(id.ToLower()) select new SeriesSearch { ExposureType = et.Name!, DateIn = sr.DateIn, ExposureDurationUnit = sr.ExposureDurationUnit, ExposureDurationVal = sr.ExposureDurationVal, LongDescription = sr.LongDescription, ShortDescription = sr.ShortDescription, seriesid = sr.seriesid, AllungaReference = sr.AllungaReference, clientreference = sr.clientreference, contactname = cl.contactname, companyname = cl.companyname }).Take(100).ToListAsync();
                 return xx;
             }

             // GET: api/Clients/5
             [HttpGet("int/{id:int}")]
             public async Task<ActionResult<Series>> GetSeries(int id)
             {

                 if (_context.Series == null)
                 {
                     return NotFound();
                 }
                 var series = await _context.Series.FindAsync(id);

                 if (series == null)
                 {
                     return NotFound();
                 }

                 return series;
             }*/

      /*  [HttpPut("{id}")]
        public async Task<IActionResult> PutSeries(int id, Series series)
        {
            if (id != series.seriesid)
            {
                return BadRequest();
            }

            _context.Entry(series).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeriesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      /*  [HttpPost]
        public async Task<ActionResult<Client>> PostSeries(Series series)
        {
            if (_context.Series == null)
            {
                return Problem("Entity set 'dbcontext.Series'  is null.");
            }
            _context.Series.Add(series);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeries", new { id = series.seriesid }, series);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeries(int id)
        {
            if (_context.Series == null)
            {
                return NotFound();
            }
            var series = await _context.Series.FindAsync(id);
            if (series == null)
            {
                return NotFound();
            }

            _context.Series.Remove(series);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeriesExists(int id)
        {
            return (_context.Series?.Any(e => e.seriesid == id)).GetValueOrDefault();
        }*/
    }
}
