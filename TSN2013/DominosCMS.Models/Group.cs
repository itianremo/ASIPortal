using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DbCore;

namespace DominosCMS.Models
{
    public class Group : EntityBase
    {
        public override int ID { get; set; }
        [Required]
        public string GroupName { get; set; }

    }
}
