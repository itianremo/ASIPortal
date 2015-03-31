using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosCMS.Models;

namespace DominosCMS.Web.Areas.Admin.Controllers
{
    public class DownloadsController : AdminController<Download>
    {
        //
        // GET: /Admin/Downloads/

        public ActionResult Index()
        {
            var model = DB.DownloadsRepository.List;
            
            return View(model);
        }

    }
}
