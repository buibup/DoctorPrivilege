using DoctorPrivilege.TextFile.Common;
using DoctorPrivilege.WebAPI.DAL.Repository;
using DoctorPrivilege.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorPrivilege.TextFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();

            //Write Local
            Helper.WriteTextFile("011");

            //Write sftp server
            //Helper.WriteTextFileToSFTPServer("011");
            //Helper.WriteTextFileToFTPServer("012");

            this.Close();
        }
    }
}
