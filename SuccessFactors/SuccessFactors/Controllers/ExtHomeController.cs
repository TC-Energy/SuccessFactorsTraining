using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuccessFactors.Models;
using static SuccessFactors.Models.StudentInfo;

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
                    emp_id = row.emp_id,
                    first_name = row.first_name,
                    last_name = row.last_name,
                    email = row.email
                });
            }
            return View(extstudents);
        }
    }
}