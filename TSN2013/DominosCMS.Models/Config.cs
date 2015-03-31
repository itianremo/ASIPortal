using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DbCore;

namespace DominosCMS.Models
{
    public class Config : EntityBase
    {
        public override int ID { get; set; }
        [Required]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
