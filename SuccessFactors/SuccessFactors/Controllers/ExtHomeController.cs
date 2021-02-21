using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuccessFactors.Models;

namespace SuccessFactors.Controllers
{
    public class ExtHomeController : Controller
    {
        // GET: ExtHome
        public ActionResult ExtHome_Index()
        {
            return View();
        }

        public ActionResult ExtStudents()
        {
            ViewBag.Message = "Your student page.";
            var data = load_employees();
            List<StudentInfo> extstudents = new List<StudentInfo>();

            foreach (var row in data)
            {
                extstudents.Add(new StudentInfo
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

            return View(extstudents);
        }
    }
}