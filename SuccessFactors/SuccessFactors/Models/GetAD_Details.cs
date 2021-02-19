using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices.AccountManagement;

namespace SuccessFactors.Models
{
    public class GetAD_Details
    {
        public string loginname { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string name { get; set; }
        public string eMail { get; set; }
        public int emp_id { get; set; }
    }
}