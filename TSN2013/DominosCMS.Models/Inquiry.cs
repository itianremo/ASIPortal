using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore;
using System.ComponentModel.DataAnnotations;

namespace DominosCMS.Models
{
    public class Inquiry : EntityBase
    {
        public override int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        public DateTime? SubmissionDate { get; set; }
    }
}
