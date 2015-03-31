using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore;
using System.ComponentModel.DataAnnotations;

namespace DominosCMS.Models
{
    public class Page : EntityBase
    {
        public override int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Url { get; set; }
        [DataType(DataType.MultilineText)]
        public string Contents { get; set; }
        [DataType(DataType.MultilineText)]
        public string Keywords { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Splash Photo")]
        public string SplashPhoto { get; set; }
        public bool Visible { get; set; }
        public bool? IsPublic { get; set; }
        public int? RuleID { get; set; }
        
 

    
    }
}
