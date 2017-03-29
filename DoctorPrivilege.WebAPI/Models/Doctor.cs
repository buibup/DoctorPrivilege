using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorPrivilege.WebAPI.Models
{
    public class Doctor
    {
        public string DoctorID { get; set; }
        public string Specialty { get; set; }
        public string SubSpecialty { get; set; }
        public string Category { get; set; }
        public string ProcedureName { get; set; }
        public string Comment { get; set; }
        public string Grade { get; set; }
        public string Status { get; set; }
        public string ApprovedDate { get; set; }
        public string ExpiredDate { get; set; }

    }
}