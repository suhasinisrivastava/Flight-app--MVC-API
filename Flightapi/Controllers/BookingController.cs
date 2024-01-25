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
    public class BookingController : ControllerBase
    {
        private readonly IBookingServ<Suhasinibooking> _bookingserv;

        public BookingController(IBookingServ<Suhasinibooking> bookingserv)
        {
            _bookingserv = bookingserv;
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suhasinibooking>>> GetSuhasinibookings()
        {
            return  _bookingserv.GetAllBookings();
        }

        // GET: api/Booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Suhasinibooking>> GetSuhasinibooking(string id)
        {
            var suhasinibooking = _bookingserv.GetBookingById(id);

            if (suhasinibooking == null)
            {
                return NotFound();
            }

            return suhasinibooking;
        }

        // PUT: api/Booking/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuhasinibooking(string id, Suhasinibooking suhasinibooking)
        {
            if (id != suhasinibooking.Bookingid)
            {
                return BadRequest();
            }

            //_context.Entry(suhasinibooking).State = EntityState.Modified;

            try
            {
                _bookingserv.UpdateBooking(id, suhasinibooking);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuhasinibookingExists(id))
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

        // POST: api/Booking
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Suhasinibooking>> PostSuhasinibooking(Suhasinibooking suhasinibooking)
        {
            //_context.Suhasinibookings.Add(suhasinibooking);
            try
            {
                _bookingserv.AddBooking(suhasinibooking);
            }
            catch (DbUpdateException)
            {
                if (SuhasinibookingExists(suhasinibooking.Bookingid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSuhasinibooking", new { id = suhasinibooking.Bookingid }, suhasinibooking);
        }

        // DELETE: api/Booking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuhasinibooking(string id)
        {
            var suhasinibooking = _bookingserv.GetBookingById(id);
            if (suhasinibooking == null)
            {
                return NotFound();
            }

            _bookingserv.DeleteBooking(id);

            return NoContent();
        }

        private bool SuhasinibookingExists(string id)
        {
            Suhasinibooking e= _bookingserv.GetBookingById(id);
            if(e!=null)
            return true;
            else
            return false;
        }
    }
}
