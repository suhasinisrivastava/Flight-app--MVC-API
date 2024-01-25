using System;
using System.Collections.Generic;

namespace flightapi.Models;

public partial class Suhasinicustomer
{
    public int Customerid { get; set; }

    public string? Customeremail { get; set; }

    public string? Customerpw { get; set; }

    public string? Customerusername { get; set; }

    public string? Customerlocation { get; set; }

    public virtual ICollection<Suhasinibooking> Suhasinibookings { get; set; } = new List<Suhasinibooking>();
}
