//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Bank
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string holder_name { get; set; }
        [Required]
        [MaxLength(9)]
        [MinLength(9)]
        public string account_no { get; set; }
        [Required]
        public Nullable<double> balance { get; set; }
        public Nullable<int> manager_id { get; set; }
    }
}