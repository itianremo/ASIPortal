using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosCMS.Models;
using DominosCMS.Repositories.Abstract;
using DominosCMS.Web.Areas.Admin.Models;

namespace DominosCMS.Web.Areas.Admin.Controllers
{
    public class PagesController : AdminController<Page>
    {
        private readonly IPageRepository pageRepository;

        public PagesController()
            : base()
        {
            pageRepository = DB.PageRepository;
        }

        //
        // GET: /Pages/


        public ViewResult Index()
        {
            return View(pageRepository.List.OrderBy(p => p.Title).ToList());
        }


        public PartialViewResult List(string orderby)
        {

            IList<Page> model = null;

            switch (orderby.ToLower())
            {
                case "url":
                    model = pageRepository.List.OrderBy(p => p.Url).ToList();
                    break;
                case "visible":
                    model = pageRepository.List.OrderByDescending(p => p.Visible).ToList();
                    break;
                default:
                    model = pageRepository.List.OrderBy(p => p.Title).ToList();
                    break;
            }

            return PartialView("_PagesList", model);
        }

        //
        // GET: /Pages/Details/5

        public ActionResult Details(string url)
        {
            var model = pageRepository.FindBy(url);

            if (model == null)
                return HttpNotFound();
            return View(pageRepository.FindBy(url));
        }
        public ActionResult Get(int id)
        {
            var model = pageRepository.FindBy(id);

            if (model == null)
                return HttpNotFound();

            return View("Details", model);
        }

        //
        // GET: /Pages/Create


        public ActionResult Create()
        {
            EditPageModel model = new EditPageModel();
            ////
            model.Rules = DB.AccountsRepository.GetAllRules();
            ////
            return View(model);
        }

        //
        // POST: /Pages/Create

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Create(EditPageModel model, HttpPostedFileBase photoFile)
        {
            string[] mimeTypes = new string[] { "image/gif", "image/jpeg", "image/png" };
            if (photoFile != null)
            {
                if (photoFile.ContentLength > 400 * 1024)
                    ModelState.AddModelError("SplashPhoto", "File size is very big");

                else if (!mimeTypes.Contains(photoFile.ContentType))
                    ModelState.AddModelError("SplashPhoto", "File type is not valid");
            }


            if (ModelState.IsValid)
            {
                pageRepository.Add(model.Page);
                if (photoFile != null)
                {
                    string filename = string.Format("{0}-{1}", model.Page.ID, Path.GetFileName(photoFile.FileName));

                   model.Page.SplashPhoto = filename;
                    string path = string.Format("{0}/{1}", ConfigurationManager.AppSettings["PagesPhotos"], filename);

                    path = Server.MapPath(path);


                    photoFile.SaveAs(path);

                }
                ////////////////////////////////
                ////save the security data 
                //var p = DB.PageRepository.FindBy(model.Page.ID);
                //p.Groups = new List<Group>();
                //p.Users = new List<Account>();
                //if (model.SelectedGroups != null)
                //foreach (var item in model.SelectedGroups)
                //{
                //    var g = DB.GroupsRepository.FindBy(item);
                //    p.Groups.Add(g);

                //}
                //if (model.SelectedUsers != null)
                //foreach (var item in model.SelectedUsers)
                //{
                //    var a = DB.AccountsRepository.FindBy(item);
                //    p.Users.Add(a);
                //}
                //////////////////////////

                DB.SaveChanges();
                //
                pageRepository.Update(model.Page);
                pageRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                //model.Groups = DB.GroupsRepository.List.OrderBy(g => g.GroupName).ToList();

                //model.Users = DB.AccountsRepository.List.OrderBy(a => a.Username).ToList();

                return View(model);
            }
        }

        //
        // GET: /Pages/Edit/5


        public ActionResult Edit(int id)
        {
            EditPageModel model = new EditPageModel();

            model.Page = pageRepository.FindBy(id);

            //model.SelectedGroups = model.Page.Groups.Select(p => p.ID).ToArray();
            //model.SelectedUsers = model.Page.Users.Select(p => p.ID).ToArray();
            ////
            model.Rules = DB.AccountsRepository.GetAllRules();
            ////
            ////
            //model.Users = DB.AccountsRepository.List.OrderBy(a => a.Username).ToList();
            return View(model);
        }

        //
        // POST: /Pages/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(EditPageModel model, HttpPostedFileBase photoFile)
        {


            string[] mimeTypes = new string[] { "image/gif", "image/jpeg", "image/png" };
            if (photoFile != null)
            {
                if (photoFile.ContentLength > 400 * 1024)
                    ModelState.AddModelError("SplashPhoto", "File size is very big");

                else if (!mimeTypes.Contains(photoFile.ContentType))
                    ModelState.AddModelError("SplashPhoto", "File type is not valid");
            }
            if (ModelState.IsValid)
            {

                
                pageRepository.Update(model.Page);
                pageRepository.Save();



                DB.SaveChanges();
                if (photoFile != null)
                {
                    string filename = string.Format("{0}-{1}", model.Page.ID, Path.GetFileName(photoFile.FileName));

                    model.Page.SplashPhoto = filename;
                    string path = string.Format("{0}/{1}", ConfigurationManager.AppSettings["PagesPhotos"], filename);

                    path = Server.MapPath(path);


                    photoFile.SaveAs(path);

                    pageRepository.Update(model.Page);
                    pageRepository.Save();
                }

                //save the security data 
                //var p = DB.PageRepository.FindBy(model.Page.ID);

                //for (int i = p.Groups.Count -1; i >= 0; i--)
                //{
                //    p.Groups.RemoveAt(i);
                //}

                //for (int i = p.Users.Count-1; i >= 0; i--)
                //{
                //    p.Users.RemoveAt(i);
                //}
                //if (model.SelectedGroups != null)
                //{

                //    foreach (var item in model.SelectedGroups)
                //    {
                //        var g = DB.GroupsRepository.FindBy(item);
                //        p.Groups.Add(g);

                //    }
                //}
                //if (model.SelectedUsers != null)
                //{
                //    foreach (var item in model.SelectedUsers)
                //    {
                //        var a = DB.AccountsRepository.FindBy(item);
                //        p.Users.Add(a);
                //    }
                //}
                DB.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {

                ////
                //model.Rules = new Dictionary<int, string>();
                //model.Rules.Add(10, "View Portal Page or Portal Menu Item");
                ////
                //model.Groups = DB.GroupsRepository.List.OrderBy(g => g.GroupName).ToList();
                //model.Users = DB.AccountsRepository.List.OrderBy(a => a.Username).ToList();

                return View(model);
            }
        } 



        public ActionResult ValidateUrl(string url, bool newItem = true)
        {

            bool isValid = pageRepository.ValidatePageUrl(url);
            return Json(new { res = isValid }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ValidateUrlUpdate(string url, string oldUrl)
        {
            bool isValid = pageRepository.ValidatePageUrl(url, oldUrl);
            return Json(new { res = isValid }, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Pages/Delete/5


        public ActionResult Delete(int id)
        {
            pageRepository.RemovePage(id);


            pageRepository.Save();

            return RedirectToAction("Index");
        }


        public ActionResult Search(string term)
        {
            //var list = pageRepository.List.Where(p => p.Title.Contains(term) || p.LeftContents.Contains(term) || p.RightContents.Contains(term) || p.TopContents.Contains(term) || p.Contents.Contains(term) || p.Description.Contains(term)).ToList();
            var list = pageRepository.List.Where(p => p.Title.Contains(term) || p.Contents.Contains(term) || p.Description.Contains(term)).ToList();
            return View(list);
        }


    }
}
