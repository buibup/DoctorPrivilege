using InterSystems.Data.CacheClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DoctorPrivilege.WebAPI.DAL
{
    public class InterSystemsDA
    {
        public static DataTable BindDataTable(string cmdString, string conString)
        {
            DataTable dt = new DataTable();

            using(var con = new CacheConnection(conString))
            {
                con.Open();
                using(var adt = new CacheDataAdapter(cmdString, con))
                {
                    adt.Fill(dt);
                }
            }

            return dt;
        }

        public static DataSet BindDataSet(string cmdString, string conString)
        {
            DataSet ds = new DataSet();

            using(var con = new CacheConnection(conString))
            {
                con.Open();
                using(var adt = new CacheDataAdapter(cmdString, con))
                {
                    adt.Fill(ds);
                }
            }

            return ds;
        }
    }
}