using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore;

namespace DominosCMS.Models
{
    public class Photo : EntityBase
    {
        public override int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public bool Visible { get; set; }

        public virtual PhotoAlbum Album { get; set; }
    }
}
