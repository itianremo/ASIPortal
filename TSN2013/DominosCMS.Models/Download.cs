using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore;

namespace DominosCMS.Models
{
    public class Download : EntityBase
    {
        public override int ID { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Filename { get; set; }
        public bool MembersOnly { get; set; }
        public int Counter { get; set; }
    }
}
