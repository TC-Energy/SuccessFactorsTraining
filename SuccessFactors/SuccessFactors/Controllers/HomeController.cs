using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuccessFactors.Models;
using static SuccessFactors.Models.StudentInfo;
using static SuccessFactors.Models.GetAD_Details;
using System.DirectoryServices.AccountManagement;

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

            SQLConnection.checkUserExists("checkUserExists", g1.emp_id, g1.firstname, g1.lastname, g1.name, "", 1, g1.eMail);

            return View();
        }
        [Authorize(Roles ="Admin")]
        public ActionResult Students()
        {
            ViewBag.Message = "Your student page.";
            var data = load_employees();
            List<StudentInfo> students = new List<StudentInfo>();

            foreach(var row in data)
            {
                students.Add(new StudentInfo
                {
                    emp_id = row.emp_id,
                    first_name = row.first_name,
                    last_name = row.last_name,
                    email = row.email
                });
            }

            var getRoles = new WebRoleProvide();
            var printRole = getRoles.GetRolesForUser("");

            if (printRole[0] == "Admin")
            {
                return View(students);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Instructors()
        {
            ViewModel mymodel = new ViewModel();

            int instructorID = Int32.Parse(UserPrincipal.Current.EmployeeId);
            var assignedCoursesInstructor = SQLConnection.InstructorAssignedCourses(instructorID);
            var taughtCoursesInstructor = SQLConnection.InstructorTaughtCourses(instructorID);

            mymodel.AllInstructorAssignCourses = assignedCoursesInstructor;
            mymodel.AllInstructorTaughtCourses = taughtCoursesInstructor;


            return View(mymodel);
        }

        public ActionResult Sessions()
        {
            ViewModel mymodel = new ViewModel();

            int instructorID = Int32.Parse(UserPrincipal.Current.EmployeeId);
            int courseID = Int32.Parse(Request.Form["CourseID"]); 
            var instructorSpecificSessions = SQLConnection.load_sessions(instructorID, courseID);
           
            mymodel.InstructorSessions = instructorSpecificSessions;

            return View(mymodel);
        }
    }
}