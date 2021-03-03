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
       
        [Display(Name = "Title")]
        public string title { get; set; }

        [Display(Name = "Date of course session")]
        public string createdON { get; set; }

        [Display(Name = "Course Session Location")]
        public string location { get; set; }

       
      
    }
}