using System;
using System.Collections.Generic;

namespace flightapi.Models;

public partial class Suhasinibooking
{
    public string Bookingid { get; set; } = null!;

    public int? Customerid { get; set; }

    public string? Bookingname { get; set; }

    public string? Flightid { get; set; }

    public DateTime? Bookingdate { get; set; }

    public int? Bookingphone { get; set; }

    public string? Bookingaddress { get; set; }

    public int? Bookingtotalcost { get; set; }

    public int? Bookingtotalmembers { get; set; }

    public virtual Suhasinicustomer? Customer { get; set; }

    public virtual Suhasiniflight? Flight { get; set; }
}
