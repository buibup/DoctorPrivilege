using DoctorPrivilege.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorPrivilege.WebAPI.DAL.Repository
{
    public interface IDoctorRepository
    {
        List<Doctor> GetAllDoctor();
        List<Doctor> GetDoctorByBUID(string BUID);
    }
}