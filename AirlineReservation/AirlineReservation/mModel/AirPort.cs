using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineReservation.mModel
{
    public class AirPortModel
    {
        public string Code { set; get; }
        public string Name { set; get; }
    }

    public class AirPort
    {
        public string Code { set; get; }
        public string Name { set; get; }
        public List<AirPortModel> StationTo { set; get; }
    }
}