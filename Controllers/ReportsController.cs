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
    public class ReportsController : ControllerBase
    {
        private readonly dbcontext _context;

        public ReportsController(dbcontext context)
        {
            _context = context;
        }

        // GET: api/Reports
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Report>>> GetReport(string id)
        {
            if (_context.Report == null)
            {
                return NotFound();
            }
            var ret = await _context.Report.Where(i => i.seriesid == Convert.ToInt32(id) && (i.deleted==null || i.deleted==false )).ToListAsync();
            return ret;
        }

        // GET: api/Reports/5
        [HttpGet("int/{id:int}")]
        public async Task<ActionResult<Report>> GetReport(int id)
        {
            if (_context.Report == null)
            {
                return NotFound();
            }
            var report = await _context.Report.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            return report;
        }

        // PUT: api/Reports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport(int id, Report report)
        {
            if (id != report.reportid)
            {
                return BadRequest();
            }

            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id))
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

        // POST: api/Reports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Report>> PostReport(Report report)
        {
            if (_context.Report == null)
            {
                return Problem("Entity set 'dbcontext.Report'  is null.");
            }
            _context.Report.Add(report);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReport", new { id = report.reportid }, report);
        }

        // DELETE: api/Reports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            if (_context.Report == null)
            {
                return NotFound();
            }
            var report = await _context.Report.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.Report.Remove(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportExists(int id)
        {
            return (_context.Report?.Any(e => e.reportid == id)).GetValueOrDefault();
        }
    }
}
