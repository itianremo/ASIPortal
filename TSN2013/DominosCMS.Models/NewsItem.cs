using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore;
using System.ComponentModel.DataAnnotations;

namespace DominosCMS.Models
{
    public class NewsItem :  EntityBase
    {
        public override int  ID { get; set; }
        [Required]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Briefe { get; set; }
        [DataType(DataType.MultilineText)]
        public string Details { get; set; }
        public string Photo { get; set; }
        public DateTime? LastUpdate { get; set; }
        [Display(Name = "Show In news side bar")]
        public bool ShowInHome { get; set; }
    }
}
