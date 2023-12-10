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
   // [Authorize]
  //  [RequiredScope("tasks.read")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly dbcontext _context;

        public ClientsController(dbcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClient()
        {
            if (_context.Client == null)
            {
                return NotFound();
            }
            var xx = await _context.Client.OrderBy(i=>i.companyname).ToListAsync();
            return xx;
        }
        // GET: api/Clients
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClient(string id)
        {
            if (_context.Client == null)
            {
                return NotFound();
            }
            List<Client> xx;
            if (id == "~")
                xx = await _context.Client.ToListAsync();
            else
                xx = await _context.Client.Where(i => i.companyname!.ToLower().Contains(id.ToLower())).ToListAsync();
            return xx;
        }

        // GET: api/Clients/5
        [HttpGet("int/{id:int}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            if (_context.Client == null)
            {
                return NotFound();
            }
            var client = await _context.Client.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.clientid)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            if (_context.Client == null)
            {
                return Problem("Entity set 'dbcontext.Client'  is null.");
            }
            _context.Client.Add(client);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CreatedAtAction("GetClient", new { id = client.clientid }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            if (_context.Client == null)
            {
                return NotFound();
            }
            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Client.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return (_context.Client?.Any(e => e.clientid == id)).GetValueOrDefault();
        }
    }
}
