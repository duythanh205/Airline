using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineReservation.mModel
{
    public enum CacheDataType
    {
        OriginAirPort = 1,

    }

    public enum TableType
    {
        Flight = 1,
        Booking = 2,
        Passenger = 3,
        FlightDetail = 4,
        All = 5
    }
}