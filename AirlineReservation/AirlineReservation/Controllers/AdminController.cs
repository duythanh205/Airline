using AirlineReservation.mModel;
using AirlineReservation.mService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AirlineReservation.Controllers
{
    public class AdminController : ApiController
    {
        AdminService service = new AdminService();

        /// <summary>
        /// Get all fligth in database table Flight
        /// </summary>
        /// <returns></returns>
        [Route("Api/Admin/v1/getallflight")]
        [HttpGet]
        public HttpResponseMessage GetAllFlight()
        {
            try
            {
                var result = service.GetAllTable<List<Flight>>(TableType.Flight);
                if (result != null && result.Count > 0)
                {
                    
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
        /// Get all booking in database table Booking
        /// </summary>
        /// <returns></returns>
        [Route("Api/Admin/v1/getallbooking")]
        [HttpGet]
        public HttpResponseMessage GetAllBooking()
        {
            try
            {
                var result = service.GetAllTable<List<Booking>>(TableType.Booking);
                if (result != null && result.Count > 0)
                {

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
        /// Get all Passenger in database table Passenger
        /// </summary>
        /// <returns></returns>
        [Route("Api/Admin/v1/getallPassenger")]
        [HttpGet]
        public HttpResponseMessage GetAllPassenger()
        {
            try
            {
                var result = service.GetAllTable<List<Passenger>>(TableType.Passenger);
                if (result != null && result.Count > 0)
                {

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
        /// Get all FlightDetail in database table FlightDetail
        /// </summary>
        /// <returns></returns>
        [Route("Api/Admin/v1/getallflightdetail")]
        [HttpGet]
        public HttpResponseMessage GetAllFlightDetail()
        {
            try
            {
                var result = service.GetAllTable<List<FligthDetail>>(TableType.FlightDetail);
                if (result != null && result.Count > 0)
                {

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
        /// Get all fligth in database table Flight
        /// </summary>
        /// <returns></returns>
        [Route("Api/Admin/v1/gettabledetail")]
        [HttpGet]
        public HttpResponseMessage GetTableDetail()
        {
            try
            {
                var result = service.GetAllTable<List<TableDetail>>(TableType.All);
                if (result != null && result.Count > 0)
                {

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
    }
}
