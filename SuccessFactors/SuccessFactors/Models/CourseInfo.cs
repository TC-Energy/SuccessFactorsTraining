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

        [Display(Name = "Instructor Name")]
        public string instructor { get; set; }

        [Display(Name = "Date of course session")]
        public string createdON { get; set; }

        [Display(Name = "Course Session Location")]
        public string location { get; set; }

        //sql connection
        public static List<CourseInfo> load_courses()
        {
            string sql = $"select CourseID, Course_Number, Title, Course_Description from Courses;";
            return SQLConnection.loadDate<CourseInfo>(sql);
        }

        public static List<CourseInfo> load_StudentCourses()
        {
            string sql = $"select m.Course_Number, m.Title, CONCAT(t.First_Name, CONCAT(' ', t.Last_Name) ) AS Instructor from Courses m INNER JOIN TC_Users t ON m.TCInstructorID = t.User_Id ";
            return SQLConnection.loadDate<CourseInfo>(sql);
        }

        
        public static List<CourseInfo> load_session()
        {
            string sql = $"select c.Title, s.courseID, s.CreatedON, s.Location from Session s INNER JOIN Courses c ON s.TCINSTRUCTOR_ID = c.TCInstructorID;";
            return SQLConnection.loadDate<CourseInfo>(sql);
        }

    }
}