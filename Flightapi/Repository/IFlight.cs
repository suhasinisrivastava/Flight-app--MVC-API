using flightapi.Models;
namespace flightapi.Repository

{
    public interface IFlight<Suhasiniflight> 
    {
        List<Suhasiniflight> GetAllFlights();
        void AddFlight(Suhasiniflight flight);
        
        void UpdateFlight(string id, Suhasiniflight flight);

        Suhasiniflight GetFlightById(string id);

        void DeleteFlight(string id);
    }
}