using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DbCore;

namespace DominosCMS.Models
{
    public class Contact : EntityBase
    {
        public override int ID { get; set; }
        [Required]
        public string Type { get; set; }
        [Display(Name="Company Name")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "ZIP Code")]
        public string ZipCode { get; set; }
    }
}
