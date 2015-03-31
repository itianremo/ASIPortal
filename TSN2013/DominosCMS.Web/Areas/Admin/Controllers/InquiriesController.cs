using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosCMS.Models;
using DominosCMS.Repositories;
using DominosCMS.Web.Areas.Admin.Controllers;

namespace DominosCMS.Web.Areas.Admin.Controllers
{
    public class InquiriesController : AdminController<Inquiry>
    {
        //
        // GET: /Inquiries/

        public ActionResult Index()
        {
            var model = DB.InquiryRepository.List.OrderByDescending(i => i.SubmissionDate).ToList();

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = DB.InquiryRepository.FindBy(id);
            return PartialView(model);
        }

        public ActionResult Delete(int id)
        {
            var model = DB.InquiryRepository.FindBy(id);
            DB.InquiryRepository.Remove(model);
            DB.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult OptOuts()
        {
            return View(DB.OptOutRepository.List.OrderByDescending(o => o.RequestDate).ToList());
        }

        public ActionResult DeleteOptOut(int id)
        {
            DB.OptOutRepository.Remove(id);
            DB.SaveChanges();

            return RedirectToAction("OptOuts");
        }
    }
}
