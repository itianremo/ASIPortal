using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DominosCMS.Models;

namespace DominosCMS.Web.Models
{
    public class EditMenuItemModel
    {
        private MenuItem _item;
        private Dictionary<string, string> _menuTypes;
        private Dictionary<string, string> _products;
        private Dictionary<string, string> _pages;
        private Dictionary<string, string> _targets;
        private Dictionary<string, string> _downloadFiles;

        public EditMenuItemModel()
        {
            _item = new MenuItem();
            _menuTypes = new Dictionary<string, string>();
            _products = new Dictionary<string, string>();
            _pages = new Dictionary<string, string>();
            _targets = new Dictionary<string, string>();
            _downloadFiles = new Dictionary<string, string>();

            _menuTypes.Add("1", "Page");
            _menuTypes.Add("2", "Product");
            _menuTypes.Add("3", "External");
            _menuTypes.Add("4", "Download");

            _targets.Add("_self", "Same window");
            _targets.Add("_blank", "New window");
            _targets.Add("_parent", "Parent window");

            using (var context = new DominosCMS.Repositories.CMSDbContext())
            {

                IList<Page> pages = context.Pages.OrderBy(p => p.Title).ToList();

                foreach (Page page in pages)
                    _pages.Add(page.Url, page.Title);

                //IList<Product> products = context.Products.OrderBy(p => p.Title).ToList();

                //foreach (Product product in products)
                //{
                //    if (string.IsNullOrEmpty(product.URL)) continue;

                //    _products.Add(product.URL, product.Title);
                //}

                IList<Download> downloads = context.Downloads.OrderBy(p => p.Title).ToList();

                foreach (var d in downloads)
                    _downloadFiles.Add(d.ID.ToString(), d.Title);


            }

        }


        public MenuItem Item
        {
            get
            {
                return _item;
            }

            set {
                _item = value;
            }
        }


        public Dictionary<string, string> MenuTypes
        {
            get
            {
                return _menuTypes;
            }
        }

        public string SelectedMenuType { get; set; }

        public Dictionary<string, string> Products
        {
            get
            {
                return _products;
            }
        }

        public string SelectedProduct { get; set; }

        public Dictionary<string, string> Pages
        {
            get
            {
                return _pages;
            }
        }
        public string SelectedDownload { get; set; }
        public Dictionary<string, string> Downloads
        {
            get
            {
                return _downloadFiles;
            }
        }
        public string SelectedPage { get; set; }

        public Dictionary<string, string> Targets
        {
            get
            {
                return _targets;
            }
        }

        public string SelectedTarget { get; set; }

        public int[] SelectedGroups { get; set; }
        public int[] SelectedUsers { get; set; }

        public IList<Group> Groups { get; set; }
        public IList<Account> Users { get; set; }

        public Dictionary<int, string> Rules { get; set; }


    }
}