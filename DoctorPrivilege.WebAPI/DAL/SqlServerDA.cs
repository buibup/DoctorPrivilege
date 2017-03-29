using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DoctorPrivilege.WebAPI.DAL
{
    public class SqlServerDA
    {
        public static DataTable BindDataTable(string cmdString, string conString)
        {
            DataTable dt = new DataTable();
            using(var con = new SqlConnection(conString))
            {
                con.Open();
                using(var adt = new SqlDataAdapter(cmdString, con))
                {
                    adt.Fill(dt);
                }
            }
            return dt;
        }

        public static DataSet BindDataSet(string cmdString, string conString)
        {
            DataSet ds = new DataSet();

            using(var con = new SqlConnection(conString))
            {
                con.Open();
                using(var adt = new SqlDataAdapter(cmdString, con))
                {
                    adt.Fill(ds);
                }
            }

            return ds;
        }

        public static string BindDataScalar(string cmdString, string conString)
        {
            string result = string.Empty;

            using(var con = new SqlConnection(conString))
            {
                using(var cmd = new SqlCommand(cmdString, con))
                {
                    result = cmd.ExecuteScalar().ToString();
                }
            }

            return result;
        }

        public static DataTable DataTableExcecuteProcedure(string cmdString, Dictionary<string, object> para, string conString)
        {
            DataTable dt = new DataTable();

            using(var con = new SqlConnection(conString))
            {
                con.Open();
                using(var cmd = new SqlCommand(cmdString, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if(para != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in para)
                            cmd.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                        using(SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dt.Load(dr);
                            return dt;
                        }
                    }
                }
            }

            return dt;
        }
    }
}