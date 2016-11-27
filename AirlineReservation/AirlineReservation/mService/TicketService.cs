using AirlineReservation.mDAO;
using AirlineReservation.mLibary;
using AirlineReservation.mModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineReservation.mService
{
    public class TicketService
    {
        TicketDAO dao = new TicketDAO();
        public SearchFlightResponse SearchFlightInfo(SearchFlightRequest reqDepart, string returnDate)
        {
            try
            {
                SearchFlightResponse res = new SearchFlightResponse()
                {
                    Depart = new List<Flight>(),
                    mReturn = new List<Flight>()
                };

                res.Depart = dao.SearchFlightInfo<List<Flight>>(reqDepart);

                if (!string.IsNullOrEmpty(returnDate))
                {
                    var reqReturn = reqDepart.CreateReqReturn(returnDate.ToDateTime("yyyyMMdd").ToStartDate());
                    res.mReturn = dao.SearchFlightInfo<List<Flight>>(reqReturn);
                }

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BookingResponse Booking(BookingRequest req)
        {
            try
            {
                BookingResponse response = new BookingResponse()
                {
                    code = 200,
                };

                string depart = dao.Booking(req.Request.Departure, req.PassengerInfo);
                string ret = string.Empty;
                if (req.Request.Return != null)
                {
                    ret = dao.Booking(req.Request.Return, req.PassengerInfo);
                }

                response.departure = getStatusResponse(depart);
                response.@return = getStatusResponse(ret);

                return response;
            }
            catch
            {
                throw;
            }
        }

        private StatusResponse getStatusResponse(string str)
        {
            try
            {
                if (str.Equals(string.Empty))
                {
                    return null;
                }
                if (string.Equals(str, "FAIL"))
                {
                    return new StatusResponse()
                    {
                        code = -999,
                        mess = "Lỗi Cơ Sở Dữ Liệu",
                        bookingCode = str
                    };
                }
                else
                {
                    return new StatusResponse()
                    {
                        code = 200,
                        mess = "Đặt hàng lượt đi thành công",
                        bookingCode = str
                    };
                }
            }
            catch
            {
                throw;
            }
        }
    }
}