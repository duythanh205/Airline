using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineReservation.mModel
{
    public class TableDetail
    {
        public string BookingID { set; get; }
        public DateTime BookingDate { set; get; }
        public decimal TotalPrice { set; get; }
        public BookingStatus Status { set; get; }

        public string FlightID { set; get; }
        public DateTime FlightDate { set; get; }
        public string SeatClass { set; get; }
        public string PriceClass { set; get; }

        public string Type { set; get; }
        public string FullName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }

    }
}