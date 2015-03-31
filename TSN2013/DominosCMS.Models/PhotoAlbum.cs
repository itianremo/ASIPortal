using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore;
using System.ComponentModel.DataAnnotations;

namespace DominosCMS.Models
{
    public class PhotoAlbum : EntityBase
    {
        public override int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int ViewOrder { get; set; }
        public bool Visible { get; set; }

        public virtual IList<Photo> Photos { get; set; }
    }
}
