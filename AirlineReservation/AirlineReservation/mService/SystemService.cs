using AirlineReservation.mDAO;
using AirlineReservation.mModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineReservation.mService
{
    public class SystemService
    {
        SystemDAO dao = new SystemDAO();
        /// <summary>
        /// Lấy tất cả danh sách sân bay đi
        /// </summary>
        /// <returns></returns>
        public T GetAllOrigin<T>()
        {
            try
            {
                return dao.GetAllOrigin<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lấy tất cả danh sách sân bay đến
        /// </summary>
        /// <returns></returns>
        public T GetAllDestination<T>(string originCode)
        {
            try
            {
                return dao.GetAllDestionation<T>(originCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}