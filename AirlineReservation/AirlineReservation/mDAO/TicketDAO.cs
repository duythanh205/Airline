using AirlineReservation.mLibary;
using AirlineReservation.mModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineReservation.mDAO
{
    public class TicketDAO
    {
        private static string querySearch = "select* from dbo.Flight where Origin = @originCode AND Destination = @destinationCode AND SeatCount > @paxCountCode AND FlightDate >= @departureDate AND FlightDate < @endDate";
        private static string queryInsertTableBooking = "insert into Booking values ('{0}','{1}','{2}','{3}')";
        private static string queryInsertTableFlightDetail = "insert into FlightDetail values ('{0}','{1}','{2}','{3}','{4}')";
        private static string queryInsertTablePassenger = "insert into Passenger values ('{0}','{1}', N'{2}','{3}','{4}')";

        /// <summary>
        /// Call DB to get all fligth available
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public T SearchFlightInfo<T>(SearchFlightRequest req)
        {
            try
            {
                object result = null;
                result = Database.GetInstance().SearchFlight(querySearch, req);

                return (T)result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Booking(FlightInfo flight, PassengerInfo person)
        {
            string RESPONSE = "FAIL";
            try
            {
                string bookingID = Lib.GenerateCode();
                string time = DateTime.Now.ToString();
                string queryTableBooking = string.Format(queryInsertTableBooking, bookingID, time, flight.priceTotal, 1);
                string queryTableFlightDetail = string.Format(queryInsertTableFlightDetail, bookingID, flight.flightNumber,
                    flight.departureTime, flight.seatClass, flight.priceClass);
                string queryTablePassenger = string.Format(queryInsertTablePassenger, bookingID, person.Type,
                    person.Name, person.email, person.Phone);


                bool result1 = Database.GetInstance().InsertTable(queryTableBooking);
                bool result2 = Database.GetInstance().InsertTable(queryTableFlightDetail);
                bool result3 = Database.GetInstance().InsertTable(queryTablePassenger);

                if (result1 == false || result2 == false || result3 == false)
                {
                    return RESPONSE;
                }
                
                return bookingID;
            }
            catch
            {
                throw;
            }
        }


    }
}