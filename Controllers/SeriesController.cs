using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AllungaWebAPI.Data;
using AllungaWebAPI.Models;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;
using Newtonsoft.Json;


namespace AllungaWebAPI.Controllers
{

    public static class Operation
    {
        public static Series CopyAll(this Series list)
        {
            Series ret = new Series();
            string tmpStr = JsonConvert.SerializeObject(list);
            ret = JsonConvert.DeserializeObject<Series>(tmpStr);
            return ret;
        }
    }

   [Authorize]
    [RequiredScope("tasks.read")]
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly dbcontext _context;
        public SeriesController(dbcontext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SeriesSearch>>> GetSeries(string id)
        {
            string[] ss = id.Split(new char[] { char.Parse(",") }, StringSplitOptions.RemoveEmptyEntries);
            string search = ss[0];
            int act =Convert.ToInt32(ss[1]);
            int inact = Convert.ToInt32(ss[2]);
            var cls = await _context.Series.Where(i => i.Lock_ComputerName == User.Identity.Name || i.Lock_DateTime<DateTime.UtcNow.AddHours(-2)).ToListAsync();
            foreach (var cl in cls)
            {
                cl.Lock_ComputerName = null;
                cl.Lock_DateTime = null;
            }
            await _context.SaveChangesAsync();

            string field = ss[3];

            if (_context.SeriesSearch == null)
            {
                return NotFound();
            }
            List<SeriesSearch> xx;
            try
            {
                xx = await (_context.SeriesSearch.FromSqlRaw("exec SeriesSearch '" + search + "'," + act.ToString() +"," + inact.ToString() + ",'"+ field + "'")).ToListAsync();
                return xx;
            }
            catch (Exception x)
            {
                throw x;
            }
                
        }


   

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
            var ret = series.CopyAll();
            if (series.Lock_ComputerName == null || series.Lock_ComputerName == "")
            {

                series!.Lock_ComputerName = User.Identity.Name;
                series!.Lock_DateTime = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            return ret;
        }

        //private async void LockSeries(int id)
        //{
        //    var serieslock =  await _context.Series.FindAsync(id);
        //    serieslock!.Lock_ComputerName = User.Identity.Name;
        //    serieslock!.Lock_DateTime = DateTime.UtcNow;
        //     await _context.SaveChangesAsync();
        //}

        [HttpGet("client/{id:int}")]
        public async Task<ActionResult<IEnumerable<Series>>> GetClientSeries(int id)
        {
            if (_context.Series == null)
            {
                return NotFound();
            }
            var series = await _context.Series.Where(i=>i.clientid== id).ToListAsync();

            if (series == null)
            {
                return NotFound();
            }

            return series;
        }

        [HttpPut("{id}")]
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
        }

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostSeries(Series series)
        {
            if (_context.Series == null)
            {
                return Problem("Entity set 'dbcontext.Series'  is null.");
            }
            _context.Series.Add(series);
            try
            {
                series.returnsreq = true;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            var rr =await _context.Report.Where(i => i.seriesid == id).CountAsync();
            if (rr > 0)
            {
                string Err= rr.ToString() + " reports are associated with this series - transfer or delete these reports first before deleting series";
                return   StatusCode(StatusCodes.Status304NotModified,
                   Err); 
            }
            var ss =await _context.Sample.Where(i => i.seriesid == id).CountAsync();
            if (rr > 0)
            {
                string Err = ss.ToString() + " Samples are associated with this series - transfer or delete these samples to another report first before deleting series";
                return StatusCode(StatusCodes.Status304NotModified,
                   Err);
            }
            series.Deleted= true;
           // _context.SaveChanges();

            /*
             * Dim Orphans As Boolean = False
        sqlReports = "select count(*) from [Report] where isnull([deleted],0) = 0 and SeriesID =" & iSelectedSereisID.ToString()

        oo = AllungaData.globals.ExecuteScalar(sqlReports)
        If (Not oo.Equals(0)) Then
            MessageBox.Show("There are " & oo.ToString() & " Reports / Returns assigned to this series.  You must delete or reassign these reports first before you are able to delete the series.")
            Orphans = True
        End If
        Dim sqlSample As String = "select count(*) from Sample where isnull([deleted],0) = 0 and SeriesID =" & iSelectedSereisID.ToString()
        oo = AllungaData.globals.ExecuteScalar(sqlSample)
        If (Not oo.Equals(0)) Then
            MessageBox.Show("There are " & oo.ToString() & " Sample assigned to this series.  You must delete these samples first before you are able to delete the series.")
            Orphans = True
        End If
        If (Orphans) Then
            Exit Sub
        End If


        sqldelete = "update series set [deleted]=-1,Active=0 where SeriesID =" & iSelectedSereisID.ToString()
        oo = AllungaData.globals.ExecuteScalar(sqldelete)
        Dim archanged As Boolean = False
        Try

            sqldelete = "update series set [AllungaReference]=[AllungaReference] + 'zzz'  where SeriesID =" & iSelectedSereisID.ToString()
            oo = AllungaData.globals.ExecuteScalar(sqldelete)
            archanged = True
        Catch ex As Exception

        End Try*/

           // _context.Series.Remove(series);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeriesExists(int id)
        {
            return (_context.Series?.Any(e => e.seriesid == id)).GetValueOrDefault();
        }
    }
}
