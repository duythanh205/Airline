using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineReservation.mModel
{
    public class ResponseData
    {
        public ResStatusCode Code { set; get; }
        public string Message { set; get; }
        public object Data { set; get; }

        public ResponseData()
        {

        }
    }

    public enum ResStatusCode
    {
        Success = 1,
        Fail = 0,

    }
}