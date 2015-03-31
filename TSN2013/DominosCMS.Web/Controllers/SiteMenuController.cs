using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using DominosCMS.Repositories;
using DominosCMS.Models;
using DominosCMS.Web.Models;

namespace DominosCMS.Web.Controllers
{   
    public class SiteMenuController : BaseController
    {

        //[OutputCache(Duration=60)]
        public ActionResult SiteMenu()
        {
           
            
            
            var model = DB.MenuRepository.List.OrderBy(menuitem => menuitem.ViewOrder)
                           .Where(menuitem => menuitem.Parent == null)
                           .Include(menuitem => menuitem.SubMenus);

            //int userRule = DB.AccountsRepository.GetUserRuleID(Environment.UserName);
            //int userRule = 10;
            int[] UserRulesID = DB.AccountsRepository.GetUserRuleID(Environment.UserName);
            ViewBag.RuleID = UserRulesID; 
            
            
            return PartialView("_Menu", model.ToList());
        }

       

        public ActionResult Social()
        {
            ViewBag.FaceBook = DB.ConfigRepository["facebook"];
            ViewBag.Twitter = DB.ConfigRepository["twitter"];
            ViewBag.GooglePlus = DB.ConfigRepository["googlePlus"];
            ViewBag.Youtube = DB.ConfigRepository["youtube"];
            ViewBag.Rss = DB.ConfigRepository["rss"];
            ViewBag.Linkedin = DB.ConfigRepository["linkedin"];
            ViewBag.Pintrest = DB.ConfigRepository["pintrest"];
            return PartialView();
        }

       
       


    }
}