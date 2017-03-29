using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorPrivilege.WebAPI.Models
{
    public class Procedure
    {
        public string ProcedureName { get; set; }
        public string Comment { get; set; }
        public string Grade { get; set; }
    }
}