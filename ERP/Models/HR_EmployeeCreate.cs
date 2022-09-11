using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    public class HR_EmployeeCreate
    {
        [Required]
        public int employee_id { get; set; }
        [Required]
        public string employee_name { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public string supervisor { get; set; }
        [Required]
        public string present_address { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string job_position { get; set; }
        [Required]
        public TimeSpan start_time { get; set; }
        [Required]
        public TimeSpan end_time { get; set; }
        [Required]
        public int hours_worked { get; set; }
        [Required]
        public DateTime employement_start_date { get; set; }
    }
}