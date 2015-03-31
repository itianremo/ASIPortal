using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosCMS.Models;
using DominosCMS.Web.Models;
using DominosCMS.Repositories;
using DbCore;
using System.Configuration;
using System.IO;

namespace DominosCMS.Web.Areas.Admin.Controllers
{
    public class BannersController : AdminController<Banner>
    {

        public BannersController() : base()
        {
			//this.bannerRepository = DB.BannerRepository;
        }

        //
        // GET: /Banners/

        public ViewResult Index()
        {
            return View(DataRepository.List.OrderBy(b => b.ViewOrder).ToList());
        }

        //
        // GET: /Banners/Details/5

        public ViewResult Details(int id)
        {
            return View(DataRepository.FindBy(id));
        }

        //
        // GET: /Banners/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Banners/Create

        [HttpPost]
        public ActionResult Create(Banner banner, HttpPostedFileBase photoFile)
        {
            string[] mimeTypes = new string[] { "image/gif", "image/jpeg", "image/png" };

            if (photoFile == null)
                ModelState.AddModelError("Photo", "You didn't select any files");

            else if (photoFile.ContentLength > 400 * 1024)
                ModelState.AddModelError("Photo", "File size is very big");

            else if (!mimeTypes.Contains(photoFile.ContentType))
                ModelState.AddModelError("Photo", "File type is not valid");


            if (ModelState.IsValid)
            {
                DataRepository.Add(banner);
                DataRepository.Save();

                string filename = string.Format("{0}-{1}", banner.ID, Path.GetFileName(photoFile.FileName));

                banner.Photo = filename;
                string path = string.Format("{0}/{1}", ConfigurationManager.AppSettings["BannersPhotos"], filename);

                path = Server.MapPath(path);


                photoFile.SaveAs(path);
                DataRepository.Save();

                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Banners/Edit/5

        public ActionResult Edit(int id)
        {
            var banner = DataRepository.FindBy(id);
            ViewBag.PhotoUrl = string.Format("{0}/{1}", ConfigurationManager.AppSettings["BannersPhotos"], banner.Photo);
            return View(banner);
        }

        //
        // POST: /Banners/Edit/5

        [HttpPost]
        public ActionResult Edit(Banner banner, HttpPostedFileBase photoFile)
        {
            string[] mimeTypes = new string[] { "image/gif", "image/jpeg", "image/png" };

            if (photoFile != null)
            {
                if (photoFile.ContentLength > 100 * 1024)
                    ModelState.AddModelError("Photo", "File size is very big");

                else if (!mimeTypes.Contains(photoFile.ContentType))
                    ModelState.AddModelError("Photo", "File type is not valid");
            }

            
            
            if (ModelState.IsValid) {
                
                if (photoFile != null)
                {

                    banner.Photo = string.Format("{0}-{1}", banner.ID, Path.GetFileName(photoFile.FileName));

                    string path = string.Format("{0}/{1}", ConfigurationManager.AppSettings["BannersPhotos"], banner.Photo);

                    path = Server.MapPath(path);
                    photoFile.SaveAs(path);
                }

                DataRepository.Update(banner);
                DataRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

    }
}

