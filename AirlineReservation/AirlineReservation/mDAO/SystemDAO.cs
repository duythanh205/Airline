using AirlineReservation.mLibary;
using AirlineReservation.mModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AirlineReservation.mDAO
{
    public class SystemDAO
    {
        /// <summary>
        /// Lấy tất cả sân bay đi
        /// </summary>
        /// <returns></returns>
        public T GetAllOrigin<T>()
        {
            try
            {
                return DataAccess.GetInstance().GetListCache<T>(CacheDataType.OriginAirPort);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gọi qua DataAcces. Lấy trong Cache tất cả sân bay đến
        /// </summary>
        /// <returns></returns>
        public T GetAllDestionation<T>(string originCode)
        {
            try
            {
                return DataAccess.GetInstance().GetAllDestination<T>(originCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}