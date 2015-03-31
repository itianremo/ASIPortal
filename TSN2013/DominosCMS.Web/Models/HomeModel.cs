using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DominosCMS.Models;

namespace DominosCMS.Web.Models
{
    public class HomeModel
    {
        
        public IList<NewsItem> News { get; set; }
        public IList<Banner> Banners { get; set; }
        public PhotoAlbum Album { get; set; }

        public Page Page { get; set; }
    }
}