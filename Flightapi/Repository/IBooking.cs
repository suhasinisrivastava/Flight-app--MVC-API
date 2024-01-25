using flightapi.Models;
namespace flightapi.Repository

{
    public interface IBooking<Suhasinibooking> 
    {
        List<Suhasinibooking> GetAllBookings();
        void AddBooking(Suhasinibooking booking);
    
        void UpdateBooking(string id, Suhasinibooking booking);

        Suhasinibooking GetBookingById(string id);

        void DeleteBooking(string id);
    }
}