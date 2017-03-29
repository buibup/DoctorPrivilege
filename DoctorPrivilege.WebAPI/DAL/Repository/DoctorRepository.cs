using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoctorPrivilege.WebAPI.Models;
using DoctorPrivilege.WebAPI.Common;
using System.Data;

namespace DoctorPrivilege.WebAPI.DAL.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        public List<Doctor> GetAllDoctor()
        {
            return GetData.GetAllDoctor();
        }

        public List<Doctor> GetDoctorByBUID(string BUID)
        {
            List<Doctor> doctors = new List<Doctor>();
            Doctor doctor = null;
            DateTime appDate;

            switch (BUID)
            {
                case "011":
                    BUID = "11";
                    break;
                case "012":
                    BUID = "12";
                    break;
                default:
                    break;
            }

            Dictionary<string, object> para = new Dictionary<string, object>();
            para.Add("buid", BUID);

            string procedureString = @"[dbo].[pDoctorPrivilege]";

            using (var result = SqlServerDA.DataTableExcecuteProcedure(procedureString, para, WebConfig.GetSQLDBConnectionString_PTSYSPROD()))
            {
                foreach(DataRow row in result.Rows)
                {
                    doctor = new Doctor();
                    doctor.DoctorID = row["DoctorID"].ToString();
                    doctor.Specialty = string.IsNullOrEmpty(row["Specialty"].ToString())? "" : row["Specialty"].ToString();
                    doctor.SubSpecialty = string.IsNullOrEmpty(row["SubSpecialty"].ToString()) ? "" : row["SubSpecialty"].ToString();
                    doctor.Category = string.IsNullOrEmpty(row["Category"].ToString()) ? "" : row["Category"].ToString();
                    doctor.ProcedureName = string.IsNullOrEmpty(row["Procedure"].ToString()) ? "" : row["Procedure"].ToString();
                    doctor.Comment = string.IsNullOrEmpty(row["Comment"].ToString()) ? "" : row["Comment"].ToString();
                    doctor.Grade = string.IsNullOrEmpty(row["Grade"].ToString()) ? "" : row["Grade"].ToString();
                    doctor.Status = string.IsNullOrEmpty(row["Status"].ToString()) ? "" : row["Status"].ToString();
                    doctor.ApprovedDate = DateTime.TryParse(row["ApprovedDate"].ToString(), out appDate) ? appDate.ToString("yyyy-MM-dd") : null;
                    doctor.ExpiredDate = "";
                    doctors.Add(doctor);
                }
                
            }

            return doctors;
        }
    }
}