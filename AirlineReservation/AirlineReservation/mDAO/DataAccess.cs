using AirlineReservation.mLibary;
using AirlineReservation.mModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AirlineReservation.mDAO
{
    public class DataAccess
    {
        private static DataAccess instance;
        private readonly static object lockObj = new object();

        private static List<AirPort> listOriginAirPort = new List<AirPort>();

        private DataAccess()
        {
            Init();
        }

        public static DataAccess GetInstance()
        {
            try
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new DataAccess();
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
       
        public void Init()
        {
            try
            {
                GetOriginAirPortFromFile();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lấy dữ liệu trong cache
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public T GetListCache<T>(CacheDataType type)
        {
            try
            {
                object result = new object();
                switch (type)
                {
                    case CacheDataType.OriginAirPort:
                        {
                            result = listOriginAirPort.ToList();
                            break;
                        }
                    default:
                        break;
                }
                return (T)result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lọc trong danh sách sân bay đi
        /// Lấy ra danh sách sân bay đến StationTo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetAllDestination<T>(string originCode)
        {
            try
            {
                object result = listOriginAirPort.FirstOrDefault(f => f.Code == originCode).StationTo.ToList();
                return (T)result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lấy danh sách các chuyến bay từ file config
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private void GetOriginAirPortFromFile(string path = null)
        {
            try
            {
                if (path == null)
                {
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AirPort.json");
                }

                if (File.Exists(path))
                {
                    listOriginAirPort = Lib.FromJson<List<AirPort>>(File.ReadAllText(path));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}