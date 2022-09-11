using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    public class HR_UserCreate
    {
        [Required]
        public string firstname { get; set; }

        [Required]
        public string lastname { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public int organization_id { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string confirm_password { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public string type { get; set; }

        [Required]
        public string address { get; set; }
        [Required]
        public string phone { get; set; }

        [Required]
        public string position { get; set; }
        [Required]
        public string email { get; set; }
    }
}