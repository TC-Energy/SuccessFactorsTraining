using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Dapper;
using System.Configuration;
using System.Data;

namespace SuccessFactors.Models
{
    public class StudentInfo
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string courses { get; set; }
        public string completion_status { get; set; }
        public int employeenumber { get; set; }
        public string section { get; set; }

        //sql connection
        public static List<StudentInfo> load_employees()
        {
            string sql = $"select id, first_name, last_name, email, courses, completion_status, employeenumber, section from dbo.StudentTable WHERE email = '{@HttpContext.Current.User.Identity.Name}';";

            return SQLConnection.loadDate<StudentInfo>(sql);
        }

    }
}