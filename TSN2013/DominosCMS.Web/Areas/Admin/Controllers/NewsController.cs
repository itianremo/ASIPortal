using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosCMS.Models;
using DominosCMS.Repositories.Abstract;

namespace DominosCMS.Web.Areas.Admin.Controllers
{
    public class NewsController : AdminController<NewsItem>
    {

        // If you are using Dependency Injection, you can delete the following constructor
        public NewsController() : base()
        {
        }

        //
        // GET: /Admin/News/

        public ViewResult Index()
        {
            return View(DataRepository.List.OrderByDescending(n => n.LastUpdate).ToList());
        }


        //
        // GET: /News/Details/5

        public ViewResult Details(int id)
        {
            NewsItem newsitem = DataRepository.FindBy(id);
            return View(newsitem);
        }

        //
        // GET: /News/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /News/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NewsItem newsitem, HttpPostedFileBase photoFile)
        {
            string[] mimeTypes = new string[] { "image/gif", "image/jpeg", "image/png" };

            if (photoFile != null)
            {
                if (photoFile.ContentLength > 50 * 1024)
                    ModelState.AddModelError("Photo", "File size is very big");

                else if (!mimeTypes.Contains(photoFile.ContentType))
                    ModelState.AddModelError("Photo", "File type is not valid");
            }


            if (ModelState.IsValid)
            {
                newsitem.LastUpdate = DateTime.Now;
                DataRepository.Add(newsitem);
                DataRepository.Save();

            }

            if (ModelState.IsValid)
            {

                if (photoFile != null)
                {

                    newsitem.Photo = string.Format("{0}-{1}", newsitem.ID, Path.GetFileName(photoFile.FileName));

                    string path = string.Format("{0}/{1}", ConfigurationManager.AppSettings["NewsPhotos"], newsitem.Photo);

                    path = Server.MapPath(path);
                    photoFile.SaveAs(path);
                }

                DataRepository.Update(newsitem);
                DataRepository.Save();
                return RedirectToAction("Index");
            }


            return View(newsitem);
        }


        // GET: /News/Edit/5

        public ActionResult Edit(int id)
        {
            NewsItem newsitem = DataRepository.FindBy(id);
            ViewBag.PhotoUrl = string.Format("{0}/{1}", ConfigurationManager.AppSettings["NewsPhotos"], newsitem.Photo);
            return View(newsitem);
        }

        //
        // POST: /News/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(NewsItem newsitem, HttpPostedFileBase photoFile)
        {

            string[] mimeTypes = new string[] { "image/gif", "image/jpeg", "image/png" };

            if (photoFile != null)
            {
                if (photoFile.ContentLength > 100 * 1024)
                    ModelState.AddModelError("Photo", "File size is very big");

                else if (!mimeTypes.Contains(photoFile.ContentType))
                    ModelState.AddModelError("Photo", "File type is not valid");
            }


            if (ModelState.IsValid)
            {

                //newsitem.LastUpdate = DateTime.Now;
                DataRepository.Update(newsitem);
                DataRepository.Save();


                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {

                if (photoFile != null)
                {

                    newsitem.Photo = string.Format("{0}-{1}", newsitem.ID, Path.GetFileName(photoFile.FileName));

                    string path = string.Format("{0}/{1}", ConfigurationManager.AppSettings["NewsPhotos"], newsitem.Photo);

                    path = Server.MapPath(path);
                    photoFile.SaveAs(path);
                }

                DataRepository.Update(newsitem);
                DataRepository.Save();
                return RedirectToAction("Index");
            }

            return View(newsitem);
        }

        //
        // GET: /News/Delete/5

        [ValidateInput(false)]
        public ActionResult Delete(int id)
        {
            NewsItem newsitem = DataRepository.FindBy(id);
            return View(newsitem);
        }

        //
        // POST: /News/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsItem newsitem = DataRepository.FindBy(id);
            DataRepository.Remove(newsitem);
            DataRepository.Save();
            return RedirectToAction("Index");
        }

    }
}
