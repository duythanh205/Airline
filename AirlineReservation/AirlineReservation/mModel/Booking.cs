using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineReservation.mModel
{
    public enum BookingStatus
    {
        Success = 1,
        Fail = 0
    }
    public class Booking
    {
        public string BookingID { set; get; }
        public DateTime BookingDate { set; get; }
        public decimal TotalPrice { set; get; }
        public BookingStatus Status { set; get; }
    }


    public class FlightInfo
    {
        //public string provider { get; set; }
        public string flightNumber { get; set; }
        //public string arrivalTime { get; set; }
        public DateTime departureTime { get; set; }
        //public string destinationCode { get; set; }
        //public string originCode { get; set; }
        public string seatClass { get; set; }
        public string priceClass { set; get; }
        public int priceTotal { get; set; }
    }

    public class Request
    {
        public FlightInfo Departure { get; set; }
        public FlightInfo Return { get; set; }
    }

    public class PassengerInfo
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string email { get; set; }
        public string Type { set; get; }
    }

    public class BookingRequest
    {
        public Request Request { get; set; }
        public PassengerInfo PassengerInfo { get; set; }

        public bool ValidData()
        {
            try
            {
                if(!ValidDataReq(this.Request.Departure))
                {
                    return false;
                }
                if(this.Request.Return != null)
                {
                    if(!ValidDataReq(this.Request.Return))
                    {
                        return false;
                    }
                }
                if(!ValidPassengerInfo(this.PassengerInfo))
                {
                    return false;
                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        private bool ValidDataReq(FlightInfo item)
        {
            try
            {
                if(item.departureTime == null)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(item.flightNumber))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(item.seatClass))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(item.priceClass))
                {
                    return false;
                }
                if (item.priceTotal <= 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                throw;
            }
        }

        private bool ValidPassengerInfo(PassengerInfo person)
        {
            try
            {
                if(string.IsNullOrEmpty(person.email))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(person.Name))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(person.Phone))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(person.Type))
                {
                    return false;
                }

                return true;
            }
            catch
            {
                throw;
            }
        }
    }



    public class StatusResponse
    {
        public int code { get; set; }
        public string mess { get; set; }
        public string bookingCode { get; set; }
    }

    public class BookingResponse
    {
        public int code { get; set; }
        public StatusResponse departure { get; set; }
        public StatusResponse @return { get; set; }
    }

}