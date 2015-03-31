using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DbCore;

namespace DominosCMS.Models
{
    public class MenuItem : EntityBase
    {
        public override int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Url { get; set; }
        public byte TypeCode { get; set; }
        
        public MenuType Type {
            get {
                switch (TypeCode)
                {
                    case 1:
                        return MenuType.Page;
                    case 2:
                        return MenuType.Product;
                    case 4:
                        return MenuType.Download;

                }

                return MenuType.External;
            }
        }

        public bool Visible { get; set; }
        public MenuItem Parent { get; set; }
        public string Target { get; set; }
        public virtual IList<MenuItem> SubMenus { get; set; }
        public string CssClass { get; set; }
        public int ViewOrder { get; set; }
        public bool? IsPublic { get; set; }
        public int? RuleID { get; set; }



    }

    public enum MenuType
    {
        Page = 1,
        Product = 2,
        External = 3,
        Download = 4
    }
}
