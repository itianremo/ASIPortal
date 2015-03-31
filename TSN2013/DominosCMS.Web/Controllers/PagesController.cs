using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosCMS.Repositories.Abstract;
using DominosCMS.Repositories;
using DominosCMS.Models;
using System.IO;
using System.Configuration;

namespace DominosCMS.Web.Controllers
{   
    public class PagesController : BaseController
    {
		// If you are using Dependency Injection, you can delete the following constructor


        //
        // GET: /Pages/

        [Authorize(Roles = "admin")]
        public ViewResult Index()
        {
            return View(DB.PageRepository.List.OrderBy(p => p.Title).ToList());
        }

        [Authorize(Roles = "admin")]
        public PartialViewResult List(string orderby)
        {

            IList<Page> model = null;

            switch (orderby.ToLower())
            { 
                case "url":
                    model = DB.PageRepository.List.OrderBy(p => p.Url).ToList();
                    break;
                case "visible":
                    model = DB.PageRepository.List.OrderByDescending(p => p.Visible).ToList();
                    break;
                default:
                    model = DB.PageRepository.List.OrderBy(p => p.Title).ToList();
                    break;
            }
            
            return PartialView("_PagesList", model);
        }

        //
        // GET: /Pages/Details/5

        
        
        public ActionResult Details(string url)
        {
            
            
            var model = DB.PageRepository.FindBy(url);
            //
            bool userHasAccess = false;
             
            if (model.IsPublic.HasValue && model.IsPublic.Value == true)
             {
                userHasAccess = true; 
             }
            else if (model.IsPublic.HasValue && model.IsPublic.Value == false)
            {
                if (model.RuleID.HasValue)
                {
                   
                    int[] UserRulesID = DB.AccountsRepository.GetUserRuleID(Environment.UserName);
                    foreach (int i in UserRulesID)
                    {
                        if (model.RuleID.Value == i)
                        {
                            userHasAccess = true;
                            break;
                        }
                    }

                }
                 //foreach (var item in model.Users)
                 //{
                 //    if (item.Username.Equals(User.Identity.Name))
                 //    {
                 //        userHasAccess = true;
                 //        break;
                 //    }
                 //}
                 //foreach (var item in model.Groups)
                 //{
                 //    if (User.IsInRole(item.GroupName))
                 //    {
                 //        userHasAccess = true;
                 //        break;
                 //    }
                 //}

             }
           
            if (!userHasAccess)
                return View("NotAuthorized");

            if (model == null)
                return HttpNotFound();
           return View(DB.PageRepository.FindBy(url));
        }


       
        public ActionResult NotAuthorized()
        {
            return View();
        }
        public ActionResult Get(int id)
        {
            var model = DB.PageRepository.FindBy(id);

            if (model == null)
                return HttpNotFound();

            return View("Details", model);
        }


        [Authorize(Roles = "admin")]
        public ActionResult ValidateUrl(string url, bool newItem = true)
        {
            
            bool isValid = DB.PageRepository.ValidatePageUrl(url);
            return Json(new { res = isValid }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ValidateUrlUpdate(string url, string oldUrl)
        {
            bool isValid = DB.PageRepository.ValidatePageUrl(url, oldUrl);
            return Json(new { res = isValid }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Download(int id)
        {
            return RedirectToAction("File", "Downloads", new {id = id});
        }

        
        public ActionResult Search(string term)
        {
            //var list = DB.PageRepository.List.Where(p => p.Title.Contains(term) || p.LeftContents.Contains(term) || p.RightContents.Contains(term) || p.TopContents.Contains(term) || p.Contents.Contains(term) || p.Description.Contains(term)).ToList();
            var list = DB.PageRepository.List.Where(p => p.Title.Contains(term) || p.Contents.Contains(term) || p.Description.Contains(term)).ToList();
            return View(list);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }


    }
}

