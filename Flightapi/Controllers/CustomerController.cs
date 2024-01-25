using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using flightapi.Models;

namespace flightapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly Ace52024Context _context;

        public CustomerController(Ace52024Context context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suhasinicustomer>>> GetSuhasinicustomers()
        {
            return await _context.Suhasinicustomers.ToListAsync();
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Suhasinicustomer>> GetSuhasinicustomer(int id)
        {
            var suhasinicustomer = await _context.Suhasinicustomers.FindAsync(id);

            if (suhasinicustomer == null)
            {
                return NotFound();
            }

            return suhasinicustomer;
        }

        // PUT: api/Customer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuhasinicustomer(int id, Suhasinicustomer suhasinicustomer)
        {
            if (id != suhasinicustomer.Customerid)
            {
                return BadRequest();
            }

            _context.Entry(suhasinicustomer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuhasinicustomerExists(id))
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

        // POST: api/Customer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Suhasinicustomer>> PostSuhasinicustomer(Suhasinicustomer suhasinicustomer)
        {
            _context.Suhasinicustomers.Add(suhasinicustomer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSuhasinicustomer", new { id = suhasinicustomer.Customerid }, suhasinicustomer);
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuhasinicustomer(int id)
        {
            var suhasinicustomer = await _context.Suhasinicustomers.FindAsync(id);
            if (suhasinicustomer == null)
            {
                return NotFound();
            }

            _context.Suhasinicustomers.Remove(suhasinicustomer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SuhasinicustomerExists(int id)
        {
            return _context.Suhasinicustomers.Any(e => e.Customerid == id);
        }
    }
}
