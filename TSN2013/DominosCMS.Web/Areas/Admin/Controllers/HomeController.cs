using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosCMS.Models;

namespace DominosCMS.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminController<Page>
    {
        //
        // GET: /Admin/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
