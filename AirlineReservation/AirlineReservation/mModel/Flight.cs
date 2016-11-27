using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AirlineReservation.mModel
{
    public class Flight
    {
        public string FlightID { set; get; }
        public string Origin { set; get; }
        public string Destination { set; get; }
        public DateTime FlightTime { set; get; }
        public string SeatClass { set; get; }
        public string PriceClass { set; get; }
        public int TotalSeat { set; get; }
        public decimal Price { set; get; }
    }

    public class SearchFlightRequest
    {
        public string originCode { set; get; }
        public string destinationCode { set; get; }
        public int paxCountCode { set; get; }
        public DateTime departureDate { set; get; }

        public static bool InvalidData(string orginCode, string destinationCode,
            int paxCountCode, string departureDate, string returnDate)
        {
            try
            {
                if(string.IsNullOrEmpty(orginCode))
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Content = new StringContent("Bad Request: " + orginCode)
                    });
                }
                if (string.IsNullOrEmpty(destinationCode))
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Content = new StringContent("Bad Request: " + destinationCode)
                    });
                }
                if (paxCountCode <= 0 || paxCountCode > 600)
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Content = new StringContent("Bad Request: " + paxCountCode)
                    });
                }
                if (string.IsNullOrEmpty(departureDate))
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Content = new StringContent("Bad Request: " + departureDate)
                    });
                }

                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static int ParsePaxCount(int paxCount)
        {
            try
            {
                int inf = paxCount % 10;
                int child = (paxCount / 10) % 10;
                int adt = (paxCount / 100) % 10;

                return adt + child + inf;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public SearchFlightRequest CreateReqReturn(DateTime returnDate)
        {
            try
            {
                return new SearchFlightRequest()
                {
                    originCode = this.destinationCode,
                    destinationCode = this.originCode,
                    paxCountCode = this.paxCountCode,
                    departureDate = returnDate
                    
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SearchFlightResponse
    {
        public List<Flight> Depart { set; get; }
        public List<Flight> mReturn { set; get; }

    }
}