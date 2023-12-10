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
    public class DispatchesController : ControllerBase
    {
        private readonly dbcontext _context;

        public DispatchesController(dbcontext context)
        {
            _context = context;
        }

        // GET: api/Dispatches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dispatch>>> GetDispatch()
        {
          if (_context.Dispatch == null)
          {
              return NotFound();
          }
            return await _context.Dispatch.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<DispatchStaff>>> GetDispatch(string id)
        {
            if (_context.Dispatch == null)
            {
                return NotFound();
            }
            return await (from ds in  _context.Dispatch join stf in _context.Staff on ds.StaffID equals stf.StaffID join st in _context.DispatchStatus on ds.Status equals st.StatusCode into gj
                          from subst in gj.DefaultIfEmpty()
                          where ds.SeriesID == Convert.ToInt32(id)
                          select new DispatchStaff {  Description = ds.Description,DispatchID = ds.DispatchID, SeriesID = ds.SeriesID, Staff=stf.StaffName,  ByRequest=ds.ByRequest, Comments=ds.Comments, Dte=ds.Dte, FullReturn_ElsePart= ds.FullReturn_ElsePart, ReexposureDate=ds.ReexposureDate, SplitFromDispatchID=ds.SplitFromDispatchID, Status=(subst==null)?"NA":subst.StatusDesc }).ToListAsync();
        }


        [HttpGet("int/{id:int}")]
        public async Task<ActionResult<Dispatch>> GetDispatch(int id)
        {
          if (_context.Dispatch == null)
          {
              return NotFound();
          }
            var dispatch = await _context.Dispatch.FindAsync(id);

            if (dispatch == null)
            {
                return NotFound();
            }

            return dispatch;
        }

        // PUT: api/Dispatches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDispatch(int id, Dispatch dispatch)
        {
            if (id != dispatch.DispatchID)
            {
                return BadRequest();
            }

            _context.Entry(dispatch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DispatchExists(id))
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

        // POST: api/Dispatches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dispatch>> PostDispatch(Dispatch dispatch)
        {
          if (_context.Dispatch == null)
          {
              return Problem("Entity set 'dbcontext.Dispatch'  is null.");
          }
            _context.Dispatch.Add(dispatch);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDispatch", new { id = dispatch.DispatchID }, dispatch);
        }

        // DELETE: api/Dispatches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDispatch(int id)
        {
            if (_context.Dispatch == null)
            {
                return NotFound();
            }
            var dispatch = await _context.Dispatch.FindAsync(id);
            if (dispatch == null)
            {
                return NotFound();
            }

            _context.Dispatch.Remove(dispatch);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DispatchExists(int id)
        {
            return (_context.Dispatch?.Any(e => e.DispatchID == id)).GetValueOrDefault();
        }
    }
}
