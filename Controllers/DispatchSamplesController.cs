using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AllungaWebAPI.Data;
using AllungaWebAPI.Models;

namespace AllungaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispatchSamplesController : ControllerBase
    {
        private readonly dbcontext _context;

        public DispatchSamplesController(dbcontext context)
        {
            _context = context;
        }

        [HttpGet("int/{id:int}")]
        public async Task<ActionResult<DispatchSample>> GetDispatch(int id)
        {
          if (_context.DispatchSample == null)
          {
              return NotFound();
          }
            var ret= await _context.DispatchSample.FindAsync(id);
            if (ret == null)
            {
                return NotFound();
            }
            return ret;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<DispatchSampleRpt>>> GetDispatch(string id)
        {
            if (_context.DispatchSample == null)
            {
                return NotFound();
            }
            return await (from ds in _context.DispatchSample join sm in _context.Sample on ds.SampleID equals sm.SampleID where ds.DispatchID == Convert.ToInt32(id) select new DispatchSampleRpt { DispatchID=ds.DispatchID, Description=ds.Description, SampeClientRef=sm.description, SampleAlRef=sm.Number }).ToListAsync(); // select new Dispatch {  Description = ds.description,DispatchID = ds.DispatchID, seriesid = ds.seriesid }).ToListAsync();
        }

        private bool DispatchExists(int id)
        {
            return (_context.DispatchSample?.Any(e => e.DispatchSampleID == id)).GetValueOrDefault();
        }
    }
}
