using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosCMS.Models;

namespace DominosCMS.Web.Areas.Admin.Controllers
{
    public class ConfigController : AdminController<Config>
    {
        //
        // GET: /Config/

        public ActionResult Index()
        {
            ViewBag.FaceBook = DB.ConfigRepository["facebook"];
            ViewBag.Twitter = DB.ConfigRepository["twitter"];
            ViewBag.GooglePlus = DB.ConfigRepository["googlePlus"];
            ViewBag.Youtube = DB.ConfigRepository["youtube"];
            ViewBag.Rss = DB.ConfigRepository["rss"];
            ViewBag.Linkedin = DB.ConfigRepository["linkedin"];
            ViewBag.Pintrest = DB.ConfigRepository["pintrest"];
            return View();
        }
        [HttpPost]
        public ActionResult Index(string facebook, string twitter, string youtube, string rss, string linkedin, string pintrest, string googlePlus)
        {
            if (ModelState.IsValid)
            {
                DB.ConfigRepository["facebook"] = facebook;
                DB.ConfigRepository["googlePlus"] = googlePlus;
                DB.ConfigRepository["twitter"] = twitter;
                DB.ConfigRepository["youtube"] = youtube;
                DB.ConfigRepository["linkedin"] = linkedin;
                DB.ConfigRepository["rss"] = rss;
                DB.ConfigRepository["pintrest"] = pintrest;

                DB.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

    }
}
