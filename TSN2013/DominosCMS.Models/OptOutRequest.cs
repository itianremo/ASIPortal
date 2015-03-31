using DbCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DominosCMS.Models
{
    public class OptOutRequest : EntityBase
    {
        public override int ID { get; set; }

        public DateTime RequestDate { get; set; }
        [Required]
        public string Email { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }
    }
}
