using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosCMS.Models;
using DominosCMS.Web.Models;


namespace DominosCMS.Web.Areas.Admin.Controllers
{
    public class SiteMenuController : AdminController<MenuItem>
    {
        public SiteMenuController()
            : base()
        {
        }

        public ViewResult Index()
        {
            var model = DB.MenuRepository.List.OrderBy(menuitem => menuitem.ViewOrder)
                            .Where(menuitem => menuitem.Parent == null);
            
            return View(model.ToList());
        }

        //
        // GET: /SiteMenu/Details/5

        public ViewResult Details(int id)
        {
            MenuItem menuitem = DB.MenuRepository.FindBy(id);
            return View(menuitem);
        }

        //
        // GET: /SiteMenu/Create

        public ActionResult Create()
        {
            //return Content("Create menu form will go here!");
            EditMenuItemModel model = new EditMenuItemModel();
            //model.Item.Parent = id;
            model.Item.ID = 0;
            model.Item.Visible = true;
            model.SelectedMenuType = "1";
            model.SelectedTarget = "_self";
            
            //
            model.Rules = DB.AccountsRepository.GetAllRules();
            //
            
            //model.Groups = DB.GroupsRepository.List.OrderBy(g => g.GroupName).ToList();
            //model.Users = DB.AccountsRepository.List.OrderBy(a => a.Username).ToList();
                 
            return PartialView(model);
        }

        //
        // POST: /SiteMenu/Create

        [HttpPost]
        public ActionResult Create(EditMenuItemModel model, int? pID)
        {
            if (ModelState.IsValid)
            {
                if (pID != null)
                    model.Item.Parent = DB.MenuRepository.FindBy(pID.Value);

                model.Item.TypeCode = Convert.ToByte(model.SelectedMenuType);

                model.Item.Target = model.SelectedTarget;
                if (model.Item.Type == MenuType.Page)
                    model.Item.Url = model.SelectedPage;
                if (model.Item.Type == MenuType.Product)
                    model.Item.Url = model.SelectedProduct;
                if (model.Item.Type == MenuType.Download)
                    model.Item.Url = model.SelectedDownload;

                //
                //
                //save the security data 
                //var p = DB.PageRepository.FindBy(model.Item.ID);
                //model.Item.Groups = new List<Group>();
                //model.Item.Users = new List<Account>();
                
                //if (model.SelectedGroups != null)
                //foreach (var item in model.SelectedGroups)
                //{
                //    var g = DB.GroupsRepository.FindBy(item);
                //    model.Item.Groups.Add(g);

                //}
                //if (model.SelectedUsers != null)
                //foreach (var item in model.SelectedUsers)
                //{
                //    var a = DB.AccountsRepository.FindBy(item);
                //    model.Item.Users.Add(a);
                //}

                //
                //
                DB.MenuRepository.Add(model.Item);
                DB.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //
        // GET: /SiteMenu/Edit/5

        public ActionResult Edit(int id)
        {
            EditMenuItemModel model = new EditMenuItemModel();
            //model.Item.Parent = id;
            model.Item = DB.MenuRepository.FindBy(id);
            model.SelectedMenuType = getMenuType(model.Item.Type);
            model.SelectedTarget = model.Item.Target;
            model.SelectedPage = model.Item.Url;
            model.SelectedProduct = model.Item.Url;
            //
            model.Rules = DB.AccountsRepository.GetAllRules();
            //
            return PartialView(model);

        }

        private string getMenuType(MenuType menuType)
        {
            switch (menuType)
            {
                case MenuType.Page:
                    return "1";
                case MenuType.Product:
                    return "2";
                case MenuType.External:
                    return "3";
                case MenuType.Download:
                    return "4";

            }

            return "1";
        }

        //
        // POST: /SiteMenu/Edit/5

        [HttpPost]
        public ActionResult Edit(EditMenuItemModel model)
        {
            //Thread.Sleep(1000);
            if (ModelState.IsValid)
            {
                model.Item.TypeCode = Convert.ToByte(model.SelectedMenuType);

                model.Item.Target = model.SelectedTarget;

                if (model.Item.Type == MenuType.Page)
                    model.Item.Url = model.SelectedPage;

                if (model.Item.Type == MenuType.Product)
                    model.Item.Url = model.SelectedProduct;

                if (model.Item.Type == MenuType.Download)
                    model.Item.Url = model.SelectedDownload;

                //
                //save the security data 
                //var p = DB.MenuRepository.FindBy(model.Item.ID);

                //for (int i = p.Groups.Count - 1; i >= 0; i--)
                //{
                //    p.Groups.RemoveAt(i);
                //}

                //for (int i = p.Users.Count - 1; i >= 0; i--)
                //{
                //    p.Users.RemoveAt(i);
                //}

                //if (model.SelectedGroups != null)
                //    foreach (var item in model.SelectedGroups)
                //{
                //    var g = DB.GroupsRepository.FindBy(item);
                //    p.Groups.Add(g);

                //}
                //if (model.SelectedUsers != null)
                //    foreach (var item in model.SelectedUsers)
                //{
                //    var a = DB.AccountsRepository.FindBy(item);
                //    p.Users.Add(a);
                //}

                DB.SaveChanges();

                //
                
                DB.MenuRepository.Update(model.Item);
                DB.SaveChanges();
                return RedirectToAction("Index");


            }
            return View(model);
        }

        //
        // GET: /SiteMenu/Delete/5

        public ActionResult Delete(int id)
        {
            MenuItem menuitem = DB.MenuRepository.FindBy(id);

            if (menuitem.SubMenus.Count > 0)
                return Content("Can not delete this item because it has sub menus!");

            DB.MenuRepository.DeleteMenuItem(id);
            DB.SaveChanges();
            return Content("Done");
        }

        
        public ActionResult GetMenuItems(int? id)
        {
            IList<MenuItem> menuitems = null;

            if (id == null)
                menuitems = DB.MenuRepository.List.Where(x => x.Parent == null).OrderBy(x => x.ViewOrder).ToList();
            else
                menuitems = DB.MenuRepository.List.Where(x => x.Parent.ID == id).OrderBy(x => x.ViewOrder).ToList();

            if (menuitems == null || menuitems.Count == 0)
                return Content("No sub menu");

            return PartialView(menuitems);
        }

        public ActionResult SortMenuList(int[] IDs)
        {
            //Thread.Sleep(2000);
            for (int i = 0; i < IDs.Length; i++)
            {
                var item = DB.MenuRepository.FindBy(IDs[i]);
                item.ViewOrder = i + 1;
            }

            DB.SaveChanges();
            return Content("Done");
        }

    }
}
