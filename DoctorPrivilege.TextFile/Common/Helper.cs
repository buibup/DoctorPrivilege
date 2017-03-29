using DoctorPrivilege.WebAPI.DAL.Repository;
using DoctorPrivilege.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Renci.SshNet;

namespace DoctorPrivilege.TextFile.Common
{
    public class Helper
    {
        private static IDoctorRepository _doctorRepositor;

        static Doctor GetPropertyDoctor()
        {
            Doctor doctor = new Doctor();
            Procedure procedure = new Procedure();
            List<Procedure> procedures = new List<Procedure>();
            List<string> proList = new List<string>();

            procedure.ProcedureName = "Procedure";
            procedure.Comment = "Comment";
            procedure.Grade = "Grade";
            procedures.Add(procedure);

            doctor.DoctorID = "Doctor ID";
            doctor.Specialty = "Specialty";
            doctor.SubSpecialty = "Sub Specialty";
            doctor.Category = "Category";
            doctor.ProcedureName = "Procedure";
            doctor.Comment = "Comment";
            doctor.Grade = "Grade";
            doctor.Status = "Status";
            doctor.ApprovedDate = "Approved Date";
            doctor.ExpiredDate = "Expired Date";

            return doctor;
        }

        static Tuple<List<string>, List<string>, List<string>> joinListProcedureToString(List<Procedure> procedures)
        {

            List<string> procedureListString = new List<string>();
            List<string> comments = new List<string>();
            List<string> grades = new List<string>();

            foreach (Procedure p in procedures)
            {
                procedureListString.Add(p.ProcedureName);
                comments.Add(p.Comment);
                grades.Add(p.Grade);
            }

            return new Tuple<List<string>, List<string>, List<string>>(procedureListString, comments, grades);
        }

        public static void WriteTextFile(string buid)
        {
            _doctorRepositor = new DoctorRepository();
            var doctors = _doctorRepositor.GetDoctorByBUID(buid);

            Doctor proDoctor = GetPropertyDoctor();
            doctors.Insert(0, proDoctor);

            string pathfile = GetPathFile(buid);

            using (StreamWriter writer = new StreamWriter(pathfile))
            {
                foreach(Doctor doctor in doctors)
                {
                    writer.WriteLine(
                        doctor.DoctorID.Trim() + "|"
                        + doctor.Specialty.Trim() + "|"
                        + doctor.SubSpecialty.Trim() + "|"
                        + doctor.Category.Trim() + "|"
                        + doctor.ProcedureName.Trim() + "|"
                        + doctor.Comment.Trim() + "|"
                        + doctor.Grade.Trim() + "|"
                        + doctor.Status.Trim() + "|"
                        + doctor.ApprovedDate + "|"
                        + doctor.ExpiredDate
                        );
                }
            }

        }

        static string GetPathFile(string buid)
        {
            string pathfile = string.Empty;
            string dateNow = DateTime.Now.ToString("yyyyMMdd");
            string path = ConfigurationManager.AppSettings["path"];
            string filename = ConfigurationManager.AppSettings["fileName"];

            switch (buid)
            {
                case "011":
                    filename = filename.Replace("{BUID}", buid);
                    filename = filename.Replace("{BUNAME}", "SVH");
                    path = path.Replace("{BUNAME}", "SVH");
                    pathfile = path + filename + dateNow + ".txt";
                    break;
                case "012":
                    filename = filename.Replace("{BUID}", buid);
                    filename = filename.Replace("{BUNAME}", "SNH");
                    path = path.Replace("{BUNAME}", "SNH");
                    pathfile = path + filename + dateNow + ".txt";
                    break;
                default:
                    break;
            }

            return pathfile;
        }

        public static void WriteTextFileToSFTPServer(string buid)
        {
            #region declare variable
            _doctorRepositor = new DoctorRepository();
            var doctors = _doctorRepositor.GetDoctorByBUID(buid);

            Doctor proDoctor = GetPropertyDoctor();
            doctors.Insert(0, proDoctor);

            #endregion

            var data = GetSFTP(buid);
            WritFileToSFTPServer(data.Item1, data.Item2, data.Item3, data.Item4, data.Item5, doctors);

        }

        static Tuple<string, string, string, string, string> GetSFTP(string buid)
        {
            //File Name:	DoctorPrivilege_{BUID}_{BUNAME}_YYYYMMDD.txt

            string dateNowString = DateTime.Now.ToString("yyyyMMdd");
            string _buname = string.Empty;


            string host = ConfigurationManager.AppSettings["host"].ToString();
            string filename = string.Empty;
            string workingdirectory = ConfigurationManager.AppSettings["workingdirectory"].ToString();
            string userName = string.Empty;
            string password = string.Empty;

            switch (buid)
            {
                case "011":

                    _buname = "SVH";
                    filename = ConfigurationManager.AppSettings["fileName"].ToString() + dateNowString;
                    filename = filename.Replace("{BUNAME}", _buname);
                    filename = filename.Replace("{BUID}", buid);
                    workingdirectory = workingdirectory + _buname + "/";

                    userName = "epmsSVH";
                    password = "svhPasskgtx";

                    break;
                case "012":

                    _buname = "SNH";
                    filename = ConfigurationManager.AppSettings["fileName"].ToString() + dateNowString;
                    filename = filename.Replace("{BUNAME}", "SNH");
                    filename = filename.Replace("{BUID}", buid);
                    workingdirectory = workingdirectory + _buname + "/";

                    userName = "epmsSNH";
                    password = "snhPasshdqp";

                    break;
                default:
                    break;
            }

            return new Tuple<string, string, string, string, string>(host, userName, password, workingdirectory,filename);
        }

        static void WritFileToSFTPServer(string host, string username, string password, string workingdirectory, string fileName, List<Doctor> doctors)
        {

            using (var client = new SftpClient(host, username, password))
            {
                client.Connect();

                client.ChangeDirectory(workingdirectory);
                
                client.Create(workingdirectory + fileName);

                string data = string.Empty;

                foreach(Doctor doctor in doctors)
                {
                    data += doctor.DoctorID.Trim() + "|"
                        + doctor.Specialty.Trim() + "|"
                        + doctor.SubSpecialty.Trim() + "|"
                        + doctor.Category.Trim() + "|"
                        + doctor.ProcedureName.Trim() + "|"
                        + doctor.Comment.Trim() + "|"
                        + doctor.Grade.Trim() + "|"
                        + doctor.Status.Trim() + "|"
                        + doctor.ApprovedDate + "|"
                        + doctor.ExpiredDate  + System.Environment.NewLine;
                }

                //เขียนข้อมูลลง .txt
                client.WriteAllText(workingdirectory + fileName, data);
            }
        }
    }
}
