using DoctorPrivilege.WebAPI.DAL;
using DoctorPrivilege.WebAPI.Models;
using InterSystems.Data.CacheClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DoctorPrivilege.WebAPI.Common
{
    public class GetData
    {
        private static string sqlConnectString = WebConfig.GetSQLDBConnectionString_PTSYSPROD();
        private static string cache89 = WebConfig.GetCacheDBConnectionString("89");


        public static List<Doctor> GetAllDoctor()
        {
            List<Doctor> doctors = new List<Doctor>();

            var dt = InterSystemsDA.BindDataTable(QueryString.GetAllDoctor(), cache89);
            doctors = Helper.DataTableToDoctorList(dt);

            return doctors;
        }

        public static List<Doctor> GetAllDoctorTest()
        {
            List<Doctor> doctors = new List<Doctor>();
            Doctor doctor = null;
            DateTime appDate;

            using (var con = new CacheConnection(cache89))
            {
                con.Open();
                using(var cmd = new CacheCommand(QueryString.GetAllDoctor(), con))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            doctor = new Doctor();
                            string doctorId = reader["DoctorID"].ToString();
                            doctor = new Doctor();
                            doctor.DoctorID = doctorId;
                            doctor.Specialty = reader["Specialty"].ToString();
                            doctor.SubSpecialty = reader["SubSpecialty"].ToString();
                            doctor.Category = reader["Category"].ToString();
                            //doctor.Procedures = GetData.GetProcedureList(doctorId);
                            doctor.Status = null;
                            doctor.ApprovedDate = DateTime.TryParse((GetData.GetApprovedDate(doctorId)), out appDate) ? appDate.ToString("yyyy-MM-dd") : null;
                            doctor.ExpiredDate = null;

                            doctors.Add(doctor);
                        }
                    }
                }
            }


            return doctors;
        }

        public static List<Procedure> GetProcedureList(string doctorId)
        {
            List<Procedure> procedureList = new List<Procedure>();
            Procedure procedure = null;

            using(var con = new SqlConnection(sqlConnectString))
            {
                con.Open();
                using(var cmd = new SqlCommand(QueryString.GetProcedure(doctorId), con))
                {
                    using(var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            procedure = new Procedure();

                            procedure.ProcedureName = reader["ProcedureName"].ToString();
                            procedure.Comment = reader["Comment"].ToString();
                            procedure.Grade = reader["Grade"].ToString();

                            procedureList.Add(procedure);
                        }
                    }
                }
            }

            return procedureList;
        }

        public static string GetApprovedDate(string doctorId)
        {
            string result = string.Empty;

            var dt = SqlServerDA.BindDataTable(QueryString.GetApprovedDate(doctorId), sqlConnectString);

            if(dt.Rows.Count > 0)
            {
                result = dt.Rows[0][0].ToString();
            }
            else
            {
                result = "";
            }

            return result;
        }
    }
}