using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Form_Models
{
    public class Transfer
    {
        [Required]
        public int product_id { get; set; }
        [Required]
        public string warehouse { get; set; }
        [Required]
        public float product_quantity { get; set; }
    }
}