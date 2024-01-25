using flightapi.Models;

namespace flightapi.Repository
{
    public class BookingRepo : IBooking<Suhasinibooking>
    {
        private readonly Ace52024Context db;
        public BookingRepo(){}

        public BookingRepo(Ace52024Context _db)
        {
            db=_db;

        }
        public void AddBooking(Suhasinibooking booking)
        {
            db.Suhasinibookings.Add(booking);
            db.SaveChanges();
        }

        public void DeleteBooking(string id)
        {
            Suhasinibooking e=db.Suhasinibookings.Find(id);
            db.Suhasinibookings.Remove(e);
            db.SaveChanges();
        }

        public List<Suhasinibooking> GetAllBookings()
        {
            return db.Suhasinibookings.ToList();

        }

        public Suhasinibooking GetBookingById(string id)
        {
            return  db.Suhasinibookings.Find(id);
        }

        public void UpdateBooking(string id, Suhasinibooking booking)
        {
            db.Suhasinibookings.Update(booking);
            db.SaveChanges();
        }
    }
}