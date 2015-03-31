using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosCMS.Models;

namespace DominosCMS.Web.Models
{
    public class RegistrationModel
    {
        public IList<string> Types { get; private set; }
        //public Company Company { get; private set; }
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [Compare("Email", ErrorMessage = "The email addresses do not match.")]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public bool AcceptTerms { get; set; }

        public RegistrationModel()
        {
            this.Types = new List<string> { "Architect", "Contractor", "Distributor", "Engineer", "Other" };

            //this.Company = new Company();
            //this.Company.Country = "USA";
        }

        //public RegistrationModel(Company company)
        //{
        //    this.Types = new List<string> { "Architect", "Contractor", "Distributor", "Engineer", "Other" };
        //    this.Company = company;
        //}
    }
}