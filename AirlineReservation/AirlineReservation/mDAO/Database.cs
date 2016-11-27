using AirlineReservation.mLibary;
using AirlineReservation.mModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AirlineReservation.mDAO
{
    public class Database
    {
        private static Database instance { set; get; }
        private static object lockObj = new object();

        private SqlConnection connect { set; get; }
        private Database()
        {
            Init();
        }

        /// <summary>
        /// Create SqlConnection and get ConnectionString
        /// </summary>
        private void Init()
        {
            try
            {
                connect = new SqlConnection();
                connect.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Sigleton
        /// </summary>
        /// <returns></returns>
        public static Database GetInstance()
        {
            try
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new Database();
                        }
                    }
                }

                return instance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call Sql Connection Open
        /// </summary>
        public void SqlConnectionOpen()
        {
            try
            {
                connect.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call Sql Connection Close
        /// </summary>
        public void SqlConnectionClose()
        {
            try
            {
                connect.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Search All flight with Request
        /// </summary>
        /// <param name="query"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public List<Flight> SearchFlight(string query, SearchFlightRequest req)
        {
            SqlDataReader reader = null;
            try
            {
                connect.Open();
                List<Flight> list = new List<Flight>();

                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@originCode", req.originCode);
                cmd.Parameters.AddWithValue("@destinationCode", req.destinationCode);
                cmd.Parameters.AddWithValue("@paxCountCode", req.paxCountCode);
                cmd.Parameters.AddWithValue("@departureDate", req.departureDate);
                cmd.Parameters.AddWithValue("@endDate", req.departureDate.ToEndDate());

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Flight()
                    {
                        FlightID = (string)reader["FlightID"],
                        Origin = (string)reader["Origin"],
                        Destination = (string)reader["Destination"],
                        FlightTime = (DateTime)reader["FlightDate"],
                        SeatClass = (string)reader["SeatClass"],
                        PriceClass = (string)reader["PriceClass"],
                        TotalSeat = Int16.Parse(reader["SeatCount"].ToString()),
                        Price = (decimal)reader["Price"],
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (connect != null)
                {
                    connect.Close();
                }
            }
        }

        /// <summary>
        /// Booking
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public bool InsertTable(string query)
        {
            try
            {
                connect.Open();
                SqlCommand command = connect.CreateCommand();
                command.CommandText = query;

                int result = command.ExecuteNonQuery();
                if (result <= 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connect != null)
                {
                    connect.Close();
                }
            }
        }

        /// <summary>
        /// Get All Flight in table Flight
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<Flight> GetALlFlight(string query)
        {
            SqlDataReader reader = null;
            try
            {
                connect.Open();

                List<Flight> list = new List<Flight>();
                SqlCommand cmd = new SqlCommand(query, connect);

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Flight()
                    {
                        FlightID = reader["FlightID"].ToString().Trim(),
                        Origin = reader["Origin"].ToString().Trim(),
                        Destination = reader["Destination"].ToString().Trim(),
                        FlightTime = (DateTime)reader["FlightDate"],
                        SeatClass = reader["SeatClass"].ToString().Trim(),
                        PriceClass = reader["PriceClass"].ToString().Trim(),
                        TotalSeat = Int16.Parse(reader["SeatCount"].ToString()),
                        Price = (decimal)reader["Price"],
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (connect != null)
                {
                    connect.Close();
                }
            }
        }


        /// <summary>
        /// Get All Passengers
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<Passenger> GetAllPassenger(string query)
        {
            SqlDataReader reader = null;
            try
            {
                connect.Open();

                List<Passenger> list = new List<Passenger>();
                SqlCommand cmd = new SqlCommand(query, connect);

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Passenger()
                    {
                        BookingID = reader["BookingID"].ToString().Trim(),
                        Email = reader["Email"].ToString().Trim(),
                        FullName = reader["Name"].ToString().Trim(),
                        Phone = reader["Phone"].ToString().Trim(),
                        Type = reader["Type"].ToString().Trim(),
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (connect != null)
                {
                    connect.Close();
                }
            }
        }

        /// <summary>
        /// Get All Booking
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<Booking> GetAllBooking(string query)
        {
            SqlDataReader reader = null;
            try
            {
                connect.Open();

                List<Booking> list = new List<Booking>();
                SqlCommand cmd = new SqlCommand(query, connect);

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int stt = (short)reader["Status"];
                    list.Add(new Booking()
                    {
                        BookingDate = (DateTime)reader["BookingTime"],
                        BookingID = reader["BookingID"].ToString().Trim(),
                        Status = (BookingStatus)(stt),
                        TotalPrice = (decimal)reader["PriceTotal"],
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (connect != null)
                {
                    connect.Close();
                }
            }
        }

        /// <summary>
        /// Get All in table FlightDetail
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<FligthDetail> GetALlFlightDetail(string query)
        {
            SqlDataReader reader = null;
            try
            {
                connect.Open();

                List<FligthDetail> list = new List<FligthDetail>();
                SqlCommand cmd = new SqlCommand(query, connect);

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new FligthDetail()
                    {
                        BookingID = reader["BookingID"].ToString().Trim(),
                        FlightID = reader["FlightID"].ToString().Trim(),
                        FlightDate = (DateTime)reader["FlightDate"],
                        SeatClass = reader["SeatClass"].ToString().Trim(),
                        PriceClass = reader["PriceClass"].ToString().Trim(),
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (connect != null)
                {
                    connect.Close();
                }
            }
        }

        /// <summary>
        /// Lấy dữ liệu từ 4 table Booking | Flight | FlghtDetail | Passenger
        /// Ứng với mỗi mã BookingCOde 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<TableDetail> GetALlTableDetail()
        {
            try
            {
                var FlightDetail = GetALlFlightDetail("select* from FlightDetail");
                var Booking = GetAllBooking("select* from Booking");
                var Passenger = GetAllPassenger("select* from Passenger");
                List<TableDetail> res = new List<TableDetail>();

                int n = Booking.Count;
                for (int i = 0; i < n; i++)
                {

                    int j = 0, n1 = Passenger.Count;
                    bool flag = false;
                    for(; j < n1; j++)
                    {
                        if(string.Equals(Booking[i].BookingID, Passenger[j].BookingID))
                        {
                            flag = true;
                            break;
                        }
                    }
                    int k = 0, n2 = FlightDetail.Count;
                    bool flag2 = false;
                    for(; k < n2; k++)
                    {
                        if (string.Equals(Booking[i].BookingID, FlightDetail[j].BookingID))
                        {
                            flag2 = true;
                            break;
                        }
                    }

                    if (flag == true && flag2 == true)
                    {
                        res.Add(new TableDetail()
                        {
                            BookingID = Booking[i].BookingID,
                            BookingDate = Booking[i].BookingDate,
                            Status = Booking[i].Status,
                            TotalPrice = Booking[i].TotalPrice,

                            Email = Passenger[j].Email,
                            FullName = Passenger[j].FullName,
                            Phone = Passenger[j].Phone,
                            Type = Passenger[j].Type,

                            PriceClass = FlightDetail[k].PriceClass,
                            SeatClass = FlightDetail[k].SeatClass,
                            FlightDate = FlightDetail[k].FlightDate,
                            FlightID = FlightDetail[k].FlightID,
                        });
                    }
                }

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}