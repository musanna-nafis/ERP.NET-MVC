using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Models
{
    public class HR_ExpenseReport
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string catagory { get; set; }
        [Required]
        public double amount { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public DateTime expense_date { get; set; }
    }
}