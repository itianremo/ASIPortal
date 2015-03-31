using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DominosCMS.Web.Models
{
    public class ContactModel
    {
        private Dictionary<string, string> _countries;
        private Dictionary<string, string> _states;

        public ContactModel()
        {
            this._countries = new Dictionary<string, string>();
            this._states = new Dictionary<string, string>();
        }
        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Phone number")]
        public string Phone { get; set; }


        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Company/Organization")]
        public string Company { get; set; }

        [Required]
        [DisplayName("Country")]
        public string Country { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        [DisplayName("Hardware details")]
        public string Hardware { get; set; }

        [Required]
        [DisplayName("Operating System")]
        public string OperatingSystem { get; set; }

        [Required]
        [DisplayName("Subject")]
        public string Subject { get; set; }

        [Required]
        [DisplayName("Additional comments/questions")]
        public string QueryDetails { get; set; }

        public string Attempt { get; set; }

        public Dictionary<string, string> Countries
        {
            get
            {

                return _countries;
            }
        }

        public Dictionary<string, string> States
        {
            get
            {

                return _states;
            }
        }

        public string PageContents { get; set; }
        public string PageTitle { get; set; }
    }
}