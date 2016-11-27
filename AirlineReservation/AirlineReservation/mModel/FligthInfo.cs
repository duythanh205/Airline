using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineReservation.mModel
{
    public class FligthDetail
    {
        public string BookingID { set; get; }
        public string FlightID { set; get; }
        public DateTime FlightDate { set; get; }
        public string SeatClass { set; get; }
        public string PriceClass { set; get; }
    }
}