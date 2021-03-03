using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuccessFactors.Models;
using static SuccessFactors.Models.StudentInfo;
using static SuccessFactors.Models.GetAD_Details;
using System.DirectoryServices.AccountManagement;
using static SuccessFactors.Models.CourseInfo;

namespace SuccessFactors.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            GetAD_Details g1 = new GetAD_Details();

            g1.loginname = Environment.UserName;
            g1.firstname = UserPrincipal.Current.GivenName;
            g1.lastname = UserPrincipal.Current.Surname;
            g1.name = UserPrincipal.Current.Name;
            g1.eMail = UserPrincipal.Current.EmailAddress;
            g1.emp_id = Int32.Parse(UserPrincipal.Current.EmployeeId);

            SQLConnection.checkUserExists("checkUserExists", g1.emp_id, g1.firstname, g1.lastname, g1.name, "", "student", g1.eMail);

            return View();
        }

        public ActionResult Students()
        {

            ViewBag.Message = "Your students page.";
            var data = load_StudentCourses();
            List<CourseInfo> courses = new List<CourseInfo>();

            foreach (var row in data)
            {
                courses.Add(new CourseInfo
                {
                    course_number = row.course_number,
                    title = row.title,
                    instructor = row.instructor
                });
            }
            return View(courses);
        }

        public ActionResult Instructors()
        {

            ViewBag.Message = "Your instructor page.";
            var data = load_courses();
            List<CourseInfo> courses = new List<CourseInfo>();

            foreach (var row in data)
            {
                courses.Add(new CourseInfo
                {
                    courseID = row.courseID,
                    course_number = row.course_number,
                    title = row.title,
                    course_description = row.course_description,
                    
                    
                });
            }

            data = load_session();
            foreach (var row in data)
            {
                courses.Add(new CourseInfo
                {
                    title = row.title,
                    createdON = row.createdON,
                    location = row.location

                });
            }

            return View(courses);
        }

        public ActionResult CreateSession()
        {
            string courseID = Request.Form["courseID"];
            string date = Request.Form["session-date"];
            string location = Request.Form["location"];
            string course_name = Request.Form["course-name"];
            string instructorID = UserPrincipal.Current.EmployeeId;
            SQLConnection.TCInstructor_CreateCourseSession(courseID, date, location, instructorID, course_name);
            return RedirectToAction("Instructors", "Home");

        }

        public ActionResult CourseEnroll()
        {
            return RedirectToAction("Students", "Home");
        }

        public ActionResult RoleSelection()
        {

            string userSelection = Request.Form["role"];

            if (userSelection.Equals("student"))
            {
                return RedirectToAction("Students", "Home");
            }

            else
            {
                return RedirectToAction("Instructors", "Home");
            }
        }
    }
}