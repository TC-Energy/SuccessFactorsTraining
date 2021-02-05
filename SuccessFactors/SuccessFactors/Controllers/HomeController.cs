using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuccessFactors.Models;
using static SuccessFactors.Models.StudentInfo;

namespace SuccessFactors.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

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