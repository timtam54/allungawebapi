﻿using System;
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
    //[Authorize]
    //[RequiredScope("tasks.read")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly dbcontext _context;

        public CommentsController(dbcontext context)
        {
            _context = context;
        }

        // GET: api/ExposureTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comments>>> GetComments()
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            return await _context.Comments.Take(100).ToListAsync();
        }

        // GET: api/ExposureTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Comments>>> Comments(int id)
        {
            if (_context.Comments == null)
            {
                return NotFound();
            }
            var ret = await _context.Comments.Where(ii=>ii.ReportID==id).ToListAsync();
           // ret.FirstOrDefault().Comment
            if (ret == null)
            {
                return NotFound();
            }

            return ret;
        }
/*
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExposureType(int id, ExposureType exposureType)
        {
            if (id != exposureType.ExposureTypeID)
            {
                return BadRequest();
            }

            _context.Entry(exposureType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExposureTypeExists(id))
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
*/
        // POST: api/ExposureTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
     /*   [HttpPost]
        public async Task<ActionResult<ExposureType>> PostExposureType(ExposureType exposureType)
        {
            if (_context.ExposureType == null)
            {
                return Problem("Entity set 'dbcontext.ExposureType'  is null.");
            }
            _context.ExposureType.Add(exposureType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExposureType", new { id = exposureType.ExposureTypeID }, exposureType);
        }

        // DELETE: api/ExposureTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExposureType(int id)
        {
            if (_context.ExposureType == null)
            {
                return NotFound();
            }
            var exposureType = await _context.ExposureType.FindAsync(id);
            if (exposureType == null)
            {
                return NotFound();
            }

            _context.ExposureType.Remove(exposureType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    */
    }
}
