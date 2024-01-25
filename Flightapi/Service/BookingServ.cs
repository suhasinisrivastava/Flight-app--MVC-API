using flightapi.Models;
using flightapi.Repository;

namespace flightapi.Service
{

    public class BookingServ : IBookingServ<Suhasinibooking>
    {
        private readonly IBooking<Suhasinibooking> bookingrepo;
        public BookingServ(){}

        public BookingServ(IBooking<Suhasinibooking> _bookingrepo)
        {
            bookingrepo=_bookingrepo;
        }
        public void AddBooking(Suhasinibooking flight)
        {
            bookingrepo.AddBooking(flight);
        }

        public void DeleteBooking(string id)
        {
            bookingrepo.DeleteBooking(id);
        }

        public List<Suhasinibooking> GetAllBookings()
        {
            return bookingrepo.GetAllBookings();
        }

        public Suhasinibooking GetBookingById(string id)
        {
             return bookingrepo.GetBookingById(id);
        }

        public void UpdateBooking(string id, Suhasinibooking flight)
        {
            bookingrepo.UpdateBooking(id,flight);
        }
    }
}