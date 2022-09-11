using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    public class Finance_changeinfo
    {
        [Required]
        [MinLength(4)]
        [MaxLength(10)]
        public string username { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        [RegularExpression(@"^01[3,5,6,7,8,9]{1}[0-9]{8}$")]
        public string phone { get; set; }
        [Required]
        public string address { get; set; }
    }
}