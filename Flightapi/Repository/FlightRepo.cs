using flightapi.Models;

namespace flightapi.Repository
{
    public class FlightRepo : IFlight<Suhasiniflight>
    {
        private readonly Ace52024Context db;
        public FlightRepo(){}

        public FlightRepo(Ace52024Context _db)
        {
            db=_db;

        }
        public void AddFlight(Suhasiniflight flight)
        {
            db.Suhasiniflights.Add(flight);
            db.SaveChanges();
        }

        public void DeleteFlight(string id)
        {
            Suhasiniflight e=db.Suhasiniflights.Find(id);
            db.Suhasiniflights.Remove(e);
            db.SaveChanges();
        }

        public List<Suhasiniflight> GetAllFlights()
        {
            return db.Suhasiniflights.ToList();

        }

        public Suhasiniflight GetFlightById(string id)
        {
            return  db.Suhasiniflights.Find(id);
        }

        public void UpdateFlight(string id, Suhasiniflight flight)
        {
            db.Suhasiniflights.Update(flight);
            db.SaveChanges();
        }
    }
}