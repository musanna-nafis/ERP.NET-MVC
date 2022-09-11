using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    public class HR_ProfileEdit
    {
        [Required]
        public string firstname { get; set; }

        [Required]
        public string lastname { get; set; }

        [Required]
        public string phone { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string address { get; set; }
    }
}