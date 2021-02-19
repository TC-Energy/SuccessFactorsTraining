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

            SQLConnection.checkUserExists("checkUserExists", g1.emp_id, g1.firstname, g1.lastname, g1.name, "asd", "Admin", g1.eMail);

            return View();
        }

        public ActionResult Students()
        {
            ViewBag.Message = "Your student page.";
            var data = load_employees();
            List<StudentInfo> students = new List<StudentInfo>();

            foreach(var row in data)
            {
                students.Add(new StudentInfo
                {
                    id = row.id,
                    first_name = row.first_name,
                    last_name = row.last_name,
                    email = row.email,
                    courses = row.courses,
                    completion_status = row.completion_status,
                    employeenumber = row.employeenumber,
                    section = row.section
                });
            }

            return View(students);
        }
    }
}