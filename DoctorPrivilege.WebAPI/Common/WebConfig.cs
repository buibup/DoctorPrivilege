using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DoctorPrivilege.WebAPI.Common
{
    public class WebConfig
    {
        public static string GetCacheDBConnectionString(string server)
        {
            if (string.IsNullOrEmpty(server))
            {
                return ConfigurationManager.ConnectionStrings["Chache89"].ToString();
            }

            if(server == "112")
            {
                return ConfigurationManager.ConnectionStrings["Chache112"].ToString();
            }
            else
            {
                return ConfigurationManager.ConnectionStrings["Chache89"].ToString();
            }
        }

        public static string GetSQLDBConnectionString_PTSYS()
        {
            return ConfigurationManager.ConnectionStrings["SVH24-SQL"].ToString();
        }

        public static string GetSQLDBConnectionString_PTSYSPROD()
        {
            return ConfigurationManager.ConnectionStrings["SVH24-SQL_PTSYSPROD"].ToString();
        }
    }
}