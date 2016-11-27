using AirlineReservation.mModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AirlineReservation.mLibary;
using AirlineReservation.mService;

namespace AirlineReservation.Controllers
{
    public class TicketController : ApiController
    {
        TicketService service = new TicketService();

        /// <summary>
        /// Tìm các chuyến bay 2 chiều theo điều kiện search
        /// </summary>
        /// <returns></returns>
        [Route("Api/Ticket/v1/{orginCode}/{destinationCode}/{paxCountCode}/{departureDate}/{returnDate?}")]
        [HttpGet]
        public HttpResponseMessage SearchFlight(string orginCode, string destinationCode,
            int paxCountCode, string departureDate, string returnDate = null)
        {
            try
            {
                bool kq = SearchFlightRequest.InvalidData(orginCode, destinationCode, paxCountCode, departureDate, returnDate);
                if (kq)
                {
                    SearchFlightRequest reqDepart = new SearchFlightRequest()
                    {
                        originCode = orginCode,
                        destinationCode = destinationCode,
                        paxCountCode = SearchFlightRequest.ParsePaxCount(paxCountCode),
                        departureDate = departureDate.ToDateTime("yyyyMMdd").ToStartDate()
                    };

                    var result = service.SearchFlightInfo(reqDepart, returnDate);

                    return Request.CreateResponse(HttpStatusCode.OK, new ResponseData()
                    {
                        Code = ResStatusCode.Success,
                        Data = result,
                        Message = Enum.GetName(typeof(ResStatusCode), Convert.ToInt32(ResStatusCode.Success))
                    });
                }
                else
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Content = new StringContent("Bad Request")
                    });
                }
            }
            catch (HttpResponseException httpEX)
            {
                return httpEX.Response;
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Internal Server Error" + ex.Message)
                };
            }
        }

        /// <summary>
        /// Đặt vé máy bay
        /// </summary>
        /// <returns></returns>
        [Route("Api/Ticket/v1/book")]
        [HttpPost]
        public HttpResponseMessage Booking([FromBody] BookingRequest req)
        {
            try
            {
                bool kq = req.ValidData();

                if (kq)
                {

                    return Request.CreateResponse(HttpStatusCode.OK, new ResponseData()
                    {
                        Code = ResStatusCode.Success,
                        Data = service.Booking(req),
                        Message = Enum.GetName(typeof(ResStatusCode), Convert.ToInt32(ResStatusCode.Success))
                    });
                }
                else
                {
                    throw new HttpResponseException(new HttpResponseMessage()
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Content = new StringContent("Bad Request")
                    });
                }
            }
            catch (HttpResponseException httpEX)
            {
                return httpEX.Response;
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent("Internal Server Error" + ex.Message)
                };
            }
        }
    }
}
