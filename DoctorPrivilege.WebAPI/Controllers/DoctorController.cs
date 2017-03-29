using DoctorPrivilege.WebAPI.DAL.Repository;
using DoctorPrivilege.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DoctorPrivilege.WebAPI.Controllers
{
    public class DoctorController : ApiController
    {
        private IDoctorRepository _doctorRepository;
        [HttpGet]
        public List<Doctor> GetAllDoctor()
        {
            _doctorRepository = new DoctorRepository();

            return _doctorRepository.GetAllDoctor();
        }

        
    }
}
