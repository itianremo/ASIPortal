using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using DbCore;

namespace DominosCMS.Models
{
    public class Account : EntityBase
    {
        public override int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public string ActivationCode { get; set; }
        public bool? Activated { get; set; }

     


    }
}
