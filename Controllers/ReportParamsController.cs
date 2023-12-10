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
    public class ReportParamsController : ControllerBase
    {
        private readonly dbcontext _context;

        public ReportParamsController(dbcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportParam>>> GetReportParam()
        {
            if (_context.ReportParam == null)
            {
                return NotFound();
            }
            return await _context.ReportParam.ToListAsync();
        }

        // GET: api/ReportParams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ReportParamDesc>>> GetReportParam(string id)
        {
            if (_context.ReportParam == null)
            {
                return NotFound();
            }
            var prms = await (from prm in _context.Param orderby prm.Ordering select new ReportParamDesc { paramid = prm.ParamID, reportid = Convert.ToInt32(id), paramname = prm.ParamName }).ToListAsync();

            var reportParam = await _context.ReportParam.Where(ii => ii.reportid == Convert.ToInt32(id)).ToListAsync();
            foreach (var item in prms)
            {
                if (reportParam.Exists(i => i.paramid == item.paramid))
                    item.selected = true;
                else
                    item.selected = false;
            }
            if (reportParam == null)
            {
                return NotFound();
            }

            return prms;
        }

        [HttpGet("int/{id}")]
        public async Task<ActionResult<IEnumerable<ReportParam>>> GetReportParam(int id)
        {
            if (_context.ReportParam == null)
            {
                return NotFound();
            }


            var reportParam = await (from rp in  _context.ReportParam join re in _context.Report on rp.reportid equals re.reportid where re.seriesid == Convert.ToInt32(id) select rp).ToListAsync();
            
            if (reportParam == null)
            {
                return NotFound();
            }

            return reportParam;
        }

        // PUT: api/ReportParams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        /*        public async Task<IActionResult> PutReportParam(int id, ReportParam reportParam)
                {
                    if (id != reportParam.id)
                    {
                        return BadRequest();
                    }

                    _context.Entry(reportParam).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ReportParamExists(id))
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
        public async Task<IActionResult> PutReportParam(int id, List<int> reportParams)//List<ReportParam> reportParams
        {
            try
            {
                var reportParamDB = await _context.ReportParam.Where(ii => ii.reportid == Convert.ToInt32(id)).ToListAsync();

                foreach (var rp in reportParams)
                {
                    ReportParam rpdb = reportParamDB.Where(ii => ii.paramid == rp && ii.reportid == id).FirstOrDefault();

                    //  ReportParam rpdb = reportParamDB.Where(ii => ii.paramid == rp.paramid && ii.reportid == rp.reportid).FirstOrDefault();
                    if (rpdb == null)
                    {
                        rpdb = new ReportParam();
                        rpdb.reportid = id;
                        rpdb.paramid = rp;// rp.paramid;
                        rpdb.reportid = id;// rp.reportid;
                        rpdb.deleted = false;// rp.deleted;
                        _context.ReportParam.Add(rpdb);
                        await _context.SaveChangesAsync();
                    }
                    //else
                    //{
                    //    rpdb.deleted = true;// rp.deleted;
                    //}
                   
                }
                foreach (var rp in reportParamDB)
                {
                    var item = reportParams.Contains(rp.paramid);//.FirstOrDefault();
                    if (!item)
                    {
                        if (rp.deleted==false)
                        {
                            rp.deleted = true;
                            await _context.SaveChangesAsync();
                        }
                    }
                //    //var item = reportParams.Where(ii => ii.paramid == rp.paramid && ii.reportid == rp.reportid).FirstOrDefault();
                //    if (item==null)
                //    {
                //        _context.ReportParam.Remove(rp);

                //    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return NoContent();
        }

        // POST: api/ReportParams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReportParam>> PostReportParam(ReportParam reportParam)
        {
            if (_context.ReportParam == null)
            {
                return Problem("Entity set 'dbcontext.ReportParam'  is null.");
            }
            _context.ReportParam.Add(reportParam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReportParam", new { id = reportParam.id }, reportParam);
        }

        // DELETE: api/ReportParams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportParam(int id)
        {
            if (_context.ReportParam == null)
            {
                return NotFound();
            }
            var reportParam = await _context.ReportParam.FindAsync(id);
            if (reportParam == null)
            {
                return NotFound();
            }

            _context.ReportParam.Remove(reportParam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportParamExists(int id)
        {
            return (_context.ReportParam?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
