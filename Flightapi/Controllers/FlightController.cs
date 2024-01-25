using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using flightapi.Models;
using flightapi.Service;

namespace flightapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightServ<Suhasiniflight> _flightserv;

        public FlightController(IFlightServ<Suhasiniflight> flightserv)
        {
            _flightserv = flightserv;
        }

        // GET: api/Flight
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suhasiniflight>>> GetSuhasiniflights()
        {
            return  _flightserv.GetAllFlights();
        }

        // GET: api/Flight/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Suhasiniflight>> GetSuhasiniflight(string id)
        {
            var suhasiniflight = _flightserv.GetFlightById(id);

            if (suhasiniflight == null)
            {
                return NotFound();
            }

            return suhasiniflight;
        }

        // PUT: api/Flight/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuhasiniflight(string id, Suhasiniflight suhasiniflight)
        {
            if (id != suhasiniflight.Flightid)
            {
                return BadRequest();
            }

           // _context.Entry(suhasiniflight).State = EntityState.Modified;

            try
            {
                _flightserv.UpdateFlight(id, suhasiniflight);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuhasiniflightExists(id))
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

        // POST: api/Flight
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Suhasiniflight>> PostSuhasiniflight(Suhasiniflight suhasiniflight)
        {
            
            try
            {
                _flightserv.AddFlight(suhasiniflight);
            }
            catch (DbUpdateException)
            {
                if (SuhasiniflightExists(suhasiniflight.Flightid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSuhasiniflight", new { id = suhasiniflight.Flightid }, suhasiniflight);
        }

        // DELETE: api/Flight/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuhasiniflight(string id)
        {
            var suhasiniflight =  _flightserv.GetFlightById(id);
            if (suhasiniflight == null)
            {
                return NotFound();
            }

            _flightserv.DeleteFlight(id);
            

            return NoContent();
        }

        private bool SuhasiniflightExists(string id)
        {
            Suhasiniflight e=_flightserv.GetFlightById(id);
            if(e!=null){
                return true;
            }
            else{
                return false;
            }
            
        }
    }
}
