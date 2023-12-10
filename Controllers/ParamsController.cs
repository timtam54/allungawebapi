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
    public class ParamsController : ControllerBase
    {
        private readonly dbcontext _context;

        public ParamsController(dbcontext context)
        {
            _context = context;
        }

        // GET: api/Params
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Param>>> GetParam()
        {
            if (_context.Param == null)
            {
                return NotFound();
            }
            return await _context.Param.ToListAsync();
        }

        // GET: api/Params/5
        [HttpGet("int/{id}")]
        public async Task<ActionResult<IEnumerable<Param>>> GetParam(string id)
        {

            if (_context.Param == null)
            {
                return NotFound();
            }
            var rdng = await _context.ReportParam.Where(rdn => rdn.reportid == Convert.ToInt32(id)).Select(o => o.paramid).ToListAsync();
            //    var rdng = await _context.Reading.Where(rdn => rdn.reportid == Convert.ToInt32(id)).Select(o => o.Paramid).ToListAsync();
            var @params = await _context.Param.Where(i => (rdng).Contains(i.ParamID)).ToListAsync();

            if (@params == null)
            {
                return NotFound();
            }

            return @params;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Param>> GetParams(int id)
        {

            if (_context.Param == null)
            {
                return NotFound();
            }
            var @param = await _context.Param.FindAsync(id);

            if (@param == null)
            {
                return NotFound();
            }

            return @param;
        }

        // PUT: api/Params/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParam(int id, Param @param)
        {
            if (id != @param.ParamID)
            {
                return BadRequest();
            }

            _context.Entry(@param).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParamExists(id))
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

        // POST: api/Params
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Param>> PostParam(Param @param)
        {
            if (_context.Param == null)
            {
                return Problem("Entity set 'dbcontext.Param'  is null.");
            }
            _context.Param.Add(@param);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParam", new { id = @param.ParamID }, @param);
        }

        // DELETE: api/Params/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParam(int id)
        {
            if (_context.Param == null)
            {
                return NotFound();
            }
            var @param = await _context.Param.FindAsync(id);
            if (@param == null)
            {
                return NotFound();
            }

            _context.Param.Remove(@param);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParamExists(int id)
        {
            return (_context.Param?.Any(e => e.ParamID == id)).GetValueOrDefault();
        }
    }
}
