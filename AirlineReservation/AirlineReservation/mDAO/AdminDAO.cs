using AirlineReservation.mModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineReservation.mDAO
{
    public class AdminDAO
    {
        private static string queryGetAllFlight = "select* from Flight";
        private static string queryGetAllBooking = "select* from Booking";
        private static string queryGetAllPassenger = "select* from Passenger";
        private static string queryGetAllFlightDetail = "select* from FlightDetail";
        private static string queryGetJoinTable = "select* from Flight";

        public T GetAllTable<T>(TableType type)
        {
            try
            {
                object result = null;
                switch (type)
                {
                    case TableType.Flight:
                        {
                            result = Database.GetInstance().GetALlFlight(queryGetAllFlight);
                            break;
                        }
                    case TableType.Booking:
                        {
                            result = Database.GetInstance().GetAllBooking(queryGetAllBooking);
                            break;
                        }
                    case TableType.Passenger:
                        {
                            result = Database.GetInstance().GetAllPassenger(queryGetAllPassenger);
                            break;
                        }
                    case TableType.FlightDetail:
                        {
                            result = Database.GetInstance().GetALlFlightDetail(queryGetAllFlightDetail);
                            break;
                        }
                    case TableType.All:
                        {
                            result = Database.GetInstance().GetALlTableDetail();
                            break;
                        }
                    default: break;
                }

                return (T)result;
            }
            catch
            {
                throw;
            }
        }
    }
}