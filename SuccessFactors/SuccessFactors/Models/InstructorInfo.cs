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
    public class InstructorInfo
    {
        [Display(Name = "Student ID")]
        public int emp_id { get; set; }
        [Display(Name = "First Name")]
        public string first_name { get; set; }
        [Display(Name = "Last Name")]
        public string last_name { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }

        //sql connection
        public static List<InstructorInfo> load_instructors()
        {
            string sql = $"select User_Id, First_Name, Last_Name, Email from TC_Users WHERE Email = '{@HttpContext.Current.User.Identity.Name}' and RoleId = 'Instructor';";
            return SQLConnection.loadDate<InstructorInfo>(sql);
        }

    }
}