using System;
using System.Collections.Generic;

namespace flightapi.Models;

public partial class Suhasiniflight
{
    public string Flightid { get; set; } = null!;

    public string? Flightname { get; set; }

    public string? Flightsource { get; set; }

    public string? Flightdestination { get; set; }

    public DateOnly? Flightdate { get; set; }

    public TimeOnly? Flightsourcetime { get; set; }

    public int? Flightprice { get; set; }

    public TimeOnly? Flightdestinationtime { get; set; }

    public virtual ICollection<Suhasinibooking> Suhasinibookings { get; set; } = new List<Suhasinibooking>();
}
