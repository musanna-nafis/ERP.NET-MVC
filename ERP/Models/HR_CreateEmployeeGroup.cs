using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    public class HR_CreateEmployeeGroup
    {
        [Required]
        public int employee_id { get; set; }
        [Required]
        public string employee_group { get; set; }
    }
}