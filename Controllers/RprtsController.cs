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

namespace AllungaWebAPI.Controllers
{
   // [Authorize]
    //[RequiredScope("tasks.read")]
    [Route("api/[controller]")]
    [ApiController]
    public class RprtsController : ControllerBase
    {
        private readonly dbcontext _context;

        public RprtsController(dbcontext context)
        {
            _context = context;
        }


        [HttpGet("ParamsAggSampleBilling")]
        public async Task<ActionResult<IEnumerable<RptParamsAggSampleBilling>>> GetRptParamsAggSampleBilling(DateTime frm, DateTime to)
        {
            //https://localhost:7147/api/Rprts/ParamsAggSampleBilling?frm=2014-01-01&to=2018-01-01
           
            if (_context.RptParamsAggSampleBilling == null)
            {
                return NotFound();
            }
            List<RptParamsAggSampleBilling> bb = await _context.RptParamsAggSampleBilling.FromSqlRaw("RptParamsAggSampleBilling '" + frm.ToString("yyyy/MM/dd") + "','" + to.ToString("yyyy/MM/dd") + "',0").ToListAsync();
            return bb;
        }

        [HttpGet("AuditSearch")]
        public async Task<ActionResult<IEnumerable<Audit>>> GetAuditSearch(DateTime frm, DateTime to,string search)
        {
            //https://localhost:7147/api/Rprts/ParamsAggSampleBilling?frm=2014-01-01&to=2018-01-01

            if (_context.Audit == null)
            {
                return NotFound();
            }
            List<Audit> bb = await _context.Audit.FromSqlRaw("AuditSearch '" + frm.ToString("yyyy/MM/dd") + "','" + to.ToString("yyyy/MM/dd") + "','"+ search + "'").ToListAsync();
            return bb;
        }

        [HttpGet("ExposureOnSiteMonthBilling")]
        public async Task<ActionResult<IEnumerable<RptExposureOnSiteMonthBilling>>> GetRptExposureOnSiteMonthBilling(DateTime frm, DateTime to)
        {
            //https://localhost:7147/api/Rprts/ExposureOnSiteMonthBilling?frm=2014-01-01&to=2018-01-01
            if (_context.RptExposureOnSiteMonthBilling == null)
            {
                return NotFound();
            }
            List<RptExposureOnSiteMonthBilling> bb = await _context.RptExposureOnSiteMonthBilling.FromSqlRaw("RptExposureOnSiteMonthBilling '" + frm.ToString("yyyy/MM/dd") + "','" + to.ToString("yyyy/MM/dd") + "'").ToListAsync();
            return bb;
        }

        [HttpGet("ExposureMovement")]
        public async Task<ActionResult<IEnumerable<RptExposureMovement>>> GetExposureMovement(DateTime frm, DateTime to)
        {
            if (_context.RptExposureMovement == null)
            {
                return NotFound();
            }
            List<RptExposureMovement> sos = await _context.RptExposureMovement.FromSqlRaw("RptExposureMovementStartOnSite 0,'" + frm.ToString("yyyy/MM/dd") + "'").ToListAsync();
            List<RptExposureMovement> bb = await _context.RptExposureMovement.FromSqlRaw("RptExposureMovement 0,'" + frm.ToString("yyyy/MM/dd") + "','" + to.ToString("yyyy/MM/dd") + "'").ToListAsync();
            return bb.Concat(sos).ToList();
        }
        [HttpGet("Rack")]
        public async Task<ActionResult<IEnumerable<RptRack>>> Get()
        {
            //https://localhost:7147/api/Rprts/Rack
            if (_context.RptRack == null)
            {
                return NotFound();
            }
            List<RptRack> bb = await _context.RptRack.FromSqlRaw("exec RackRpt").ToListAsync();
            return bb;
        }
        /*
         Dim da As New ReturnExposureMovementDSTableAdapters.ExposureMovTableAdapter()
        da.Connection = New System.Data.SqlClient.SqlConnection(CONNECTION_STRING())
        ds.Clear()
        da.FillByStartOnSite(ds.ExposureMov, StartDate.ToString("dd-MMM-yyyy"), ClientID, StartDate)
        da.ClearBeforeFill = False
        da.Fill(ds.ExposureMov, StartDate, EndDate, ClientID)
        ds.ExposureMov.RunningTotalForSeriesColumn.ReadOnly = False
        ''Return
        For Each item As ReturnExposureMovementDS.ExposureMovRow In ds.ExposureMov.Select("RunningTotalForSeries<>-1")
            Dim SeriesID As Integer = item.SeriesID
            Dim ExpType As String = item.ExpType
            Dim RS As Integer = item.RunningTotalForSeries
            Dim rows() As ReturnExposureMovementDS.ExposureMovRow
            rows = ds.ExposureMov.Select("RunningTotalForSeries=-1 and SeriesID=" & SeriesID.ToString & " and ExpType='" & ExpType & "'", "DTE asc")
            If (OnlySeriesWithMovement) And (rows.Length = 0) Then
                item.Delete()
            Else

                For Each subitem As ReturnExposureMovementDS.ExposureMovRow In rows
                    If subitem.ExpRet = "Exposure" Then
                        RS = RS + subitem.equivs
                    Else
                        RS = RS - subitem.equivs
                    End If
                    subitem.RunningTotalForSeries = RS
                Next
            End If
        Next
         */
        [HttpGet("SampleOnSite")]
        public async Task<ActionResult<IEnumerable<RptSampleOnSite>>> GetRptSampleOnSite()
        {
            if (_context.RptSampleOnSite == null)
            {
                return NotFound();
            }
            return await _context.RptSampleOnSite.ToListAsync();
        }

       
    }
}
