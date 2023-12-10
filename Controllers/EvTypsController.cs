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
    public class EvTypsController : ControllerBase
    {
        private readonly dbcontext _context;

        public EvTypsController(dbcontext context)
        {
            _context = context;
        }

        // GET: api/Params
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvTyp>>> GetEvTyp()
        {
            if (_context.EvTyp == null)
            {
                return NotFound();
            }
            return await _context.EvTyp.ToListAsync();
        }

        
    }
}
