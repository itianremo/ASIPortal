using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore;
using System.ComponentModel.DataAnnotations;

namespace DominosCMS.Models
{
    public class Banner : EntityBase
    {
        public override int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Details { get; set; }
        public string Photo { get; set; }
        [Display(Name="Show in Home")]
        public bool ShowInHome { get; set; }
        [Display(Name = "View Order")]
        public int ViewOrder { get; set; }
    }
}
