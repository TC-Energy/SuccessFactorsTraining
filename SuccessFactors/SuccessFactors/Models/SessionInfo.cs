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
    public class SessionInfo
    {
        [Display(Name = "Course ID")]
        public int courseID { get; set; }

        [Display(Name = "Date Created")]
        public string createdON { get; set; }

        [Display(Name = "Location")]
        public string location { get; set; }

        [Display(Name = "Email")]
        public string email { get; set; }

     

    }
}