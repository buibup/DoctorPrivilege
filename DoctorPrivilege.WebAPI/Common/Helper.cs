using DoctorPrivilege.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DoctorPrivilege.WebAPI.Common
{
    public class Helper
    {
        public static List<Doctor> DataTableToDoctorList(DataTable dt)
        {
            List<Doctor> doctors = new List<Doctor>();
            Doctor doctor = null;
            DateTime appDate;
            int countRow = 0;
            int currentRow = 0;

            foreach (DataRow row in dt.Rows)
            {
                countRow = dt.Rows.Count;
                currentRow += 1;
                string doctorId = row["DoctorID"].ToString();
                doctor = new Doctor();
                doctor.DoctorID = doctorId;
                doctor.Specialty = row["Specialty"].ToString();
                doctor.SubSpecialty = row["SubSpecialty"].ToString();
                doctor.Category = row["Category"].ToString();
                //doctor.Procedures = GetData.GetProcedureList(doctorId);
                doctor.Status = null;
                doctor.ApprovedDate = DateTime.TryParse((GetData.GetApprovedDate(doctorId)), out appDate) ? appDate.ToString("yyyy-MM-dd") : null;
                doctor.ExpiredDate = null;

                doctors.Add(doctor);
            }

            return doctors;
        }
    }
}