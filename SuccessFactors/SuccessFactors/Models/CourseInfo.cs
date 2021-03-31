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
    public class CourseInfo
    {
        [Display(Name = "Course ID")]
        public int courseID { get; set; }
        [Display(Name = "Course Number")]
        public string course_number { get; set; }
        [Display(Name = "Course Title")]
        public string title { get; set; }
        [Display(Name = "Course Description")]
        public string course_description { get; set; }


        //sql connection
        public static List<StudentInfo> load_employees()
        {
            string sql = $"select User_Id, First_Name, Last_Name, Email from TC_Users WHERE Email = '{@HttpContext.Current.User.Identity.Name}';";
            return SQLConnection.loadDate<StudentInfo>(sql);
        }

    }
}