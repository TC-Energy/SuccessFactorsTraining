using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Dapper;
using System.Configuration;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace SuccessFactors.Models
{
    public class StudentInfo
    {
        [Display(Name = "Student ID")]
        public int id { get; set; }
        [Display(Name = "First Name")]
        public string first_name { get; set; }
        [Display(Name = "Last Name")]
        public string last_name { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Courses")]
        public string courses { get; set; }
        [Display(Name = "Status")]
        public string completion_status { get; set; }
        [Display(Name = "Employee Number")]
        public int employeenumber { get; set; }
        [Display(Name = "Section")]
        public string section { get; set; }

        //sql connection
        public static List<StudentInfo> load_employees()
        {
            string sql = $"select id, first_name, last_name, email, courses, completion_status, employeenumber, section from dbo.StudentTable WHERE email = '{@HttpContext.Current.User.Identity.Name}';";
            return SQLConnection.loadDate<StudentInfo>(sql);
        }

    }
}