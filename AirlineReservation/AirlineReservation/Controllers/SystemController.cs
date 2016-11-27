using AirlineReservation.mModel;
using AirlineReservation.mService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace AirlineReservation.Controllers
{
    public class SystemController : ApiController
    {
        SystemService systemService = new SystemService();
        /// <summary>
        /// Lấy danh sách các sân bay đi
        /// </summary>
        /// <returns></returns>
        [Route("Api/System/v1/GetOrigin")]
        [HttpGet]
        public HttpResponseMessage GetOrigin()
        {
            try
            {
                var result = systemService.GetAllOrigin<List<AirPort>>();
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseData()
                {
                    Code = ResStatusCode.Success,
                    Data = result,
                    Message = Enum.GetName(typeof(ResStatusCode), Convert.ToInt32(ResStatusCode.Success))
                });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new ResponseData()
                {
                    Code = ResStatusCode.Fail,
                    Data = ex,
                    Message = Enum.GetName(typeof(ResStatusCode), Convert.ToInt32(ResStatusCode.Fail))
                });
            }
        }

        /// <summary>
        /// Lấy danh sách các sân bay đến
        /// </summary>
        /// <returns></returns>
        [Route("Api/System/v1/GetDestination/{*origincode}")]
        [HttpGet]
        public HttpResponseMessage GetDestination()
        {
            try
            {
                string originCode = HttpContext.Current.Request.QueryString["origincode"];

                var result = systemService.GetAllDestination<List<AirPortModel>>(originCode);
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseData()
                {
                    Code = ResStatusCode.Success,
                    Data = result,
                    Message = Enum.GetName(typeof(ResStatusCode), Convert.ToInt32(ResStatusCode.Success))
                });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new ResponseData()
                {
                    Code = ResStatusCode.Fail,
                    Data = ex,
                    Message = Enum.GetName(typeof(ResStatusCode), Convert.ToInt32(ResStatusCode.Fail))
                });
            }
        }
    }
}
