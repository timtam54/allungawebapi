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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Drawing;
using System.Runtime.Intrinsics.X86;

namespace AllungaWebAPI.Controllers
{
    [Authorize]
    [RequiredScope("tasks.read")]
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesEventController : ControllerBase
    {
        private readonly dbcontext _context;

        public SeriesEventController(dbcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeriesEvent>>> GetSeriesEvent()
        {
            if (_context.SeriesEvent == null)
            {
                return NotFound();
            }
            return await _context.SeriesEvent.ToListAsync();
        }

        // GET: api/ReportParams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SeriesEventRpt>>> GetSeriesEvent(string id)
        {
            if (_context.SeriesEvent == null)
            {
                return NotFound();
            }
            int serid = Convert.ToInt32(id);
            var ser = _context.SeriesEvent.Where(ii => ii.SeriesID == serid).ToList();
            var EvTyp = _context.EvTyp.ToList();
           var prms =   (  from et in EvTyp
                           join se in ser on et.ID equals se.EventType into gj
    from subse in gj.DefaultIfEmpty()
    

                                              select new SeriesEventRpt
    {
     EventDesc=et.EventDesc!,
       EventType=et.ID
         ,      EstNextDate=(subse==null)?null:subse.EstNextDate
         ,       FrequencyUnit= (subse == null) ? null : subse.FrequencyUnit
         ,        FrequencyVal=(subse==null)?-1:subse.FrequencyVal
       ,  Id= (subse == null) ? -1 : subse.Id
       ,
                                                  SeriesID =serid
    }).ToList();


           // var prms = await (from se in _context.SeriesEvent join et in _context.EvTyp on se.EventType equals et.ID where se.SeriesID==Convert.ToInt32(id) orderby et.EventDesc select new SeriesEvent { EstNextDate= se.EstNextDate, EventType= et.EventDesc!, FrequencyUnit=se.FrequencyUnit, FrequencyVal=se.FrequencyVal, SeriesID=se.SeriesID, Id=se.Id  }).ToListAsync();

            

            return prms;
        }
/*
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
*/

        // PUT: api/ReportParams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
       
        public async Task<ActionResult<IEnumerable<SeriesEventRpt>>> PutSeriesEvent(int id, List<SeriesEventRpt> SeriesEventRpts)
        {
            var DB = await _context.SeriesEvent.Where(ii => ii.SeriesID == Convert.ToInt32(id)).ToListAsync();

            foreach (SeriesEventRpt item in SeriesEventRpts)
            {
                if (item.Id==-1)
                {
                    if (item.FrequencyVal != -1)
                    {
                        ;//add new
                        SeriesEvent seadd = new SeriesEvent();
                        seadd.SeriesID = id;
                        if (item.FrequencyUnit == null)
                            seadd.FrequencyUnit = "Hour";
                        else
                            seadd.FrequencyUnit = item.FrequencyUnit;
                        seadd.FrequencyVal = item.FrequencyVal.Value;
                        seadd.EventType = item.EventType;
                        //  seadd.EstNextDate = item.EstNextDate;
                        _context.Add(seadd);
                        await _context.SaveChangesAsync();
                    }
                }
                else if (item.FrequencyVal==0)
                {
                    var sedel=DB.Where(i => i.EventType == item.EventType).FirstOrDefault();
                    if (sedel!=null)
                    {
                        _context.SeriesEvent.Remove(sedel);
                        await _context.SaveChangesAsync();
                    }
                    ;//delete
                }
                else //update
                {
                    var db = DB.Where(i => i.Id == item.Id).FirstOrDefault();
                    db.FrequencyUnit = item.FrequencyUnit;
                    db.FrequencyVal = item.FrequencyVal.Value;
                   // db.EstNextDate
                    ReportParam rp = new ReportParam();
                   
                    await _context.SaveChangesAsync();
                }
            }
            return await GetSeriesEvent(id.ToString());
//            return CreatedAtAction("GetSeriesEvent", new { id = id }, SeriesEventRpts);
        }
/*
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
        }*/
    }
}
