using flightapi.Models;
using flightapi.Repository;

namespace flightapi.Service
{

    public class FlightServ : IFlightServ<Suhasiniflight>
    {
        private readonly IFlight<Suhasiniflight> flightrepo;
        public FlightServ(){}

        public FlightServ(IFlight<Suhasiniflight> _flightrepo)
        {
            flightrepo=_flightrepo;
        }
        public void AddFlight(Suhasiniflight flight)
        {
            flightrepo.AddFlight(flight);
        }

        public void DeleteFlight(string id)
        {
            flightrepo.DeleteFlight(id);
        }

        public List<Suhasiniflight> GetAllFlights()
        {
            return flightrepo.GetAllFlights();
        }

        public Suhasiniflight GetFlightById(string id)
        {
             return flightrepo.GetFlightById(id);
        }

        public void UpdateFlight(string id, Suhasiniflight flight)
        {
            flightrepo.UpdateFlight(id,flight);
        }
    }
}