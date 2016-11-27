using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineReservation.mModel
{
    public class Passenger
    {
        public string BookingID { set; get; }
        public string Type { set; get; }
        public string FullName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
    }
}