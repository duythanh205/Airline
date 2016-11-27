using AirlineReservation.mDAO;
using AirlineReservation.mModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirlineReservation.mService
{
    public class AdminService
    {
        AdminDAO dao = new AdminDAO();
        public T GetAllTable<T>(TableType type)
        {
            try
            {
                return dao.GetAllTable<T>(type);
            }
            catch
            {
                throw;
            }
        } 
    }
}