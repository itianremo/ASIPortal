using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosCMS.Web.Helpers;
using System.Configuration;

namespace DominosCMS.Web.Controllers
{
    public class DownloadsController : BaseController
    {
        //
        // GET: /Downloads/

        public ActionResult File(int id)
        {
            var d = DB.DownloadsRepository.FindBy(id);

            string path = string.Format("{0}/{1}/{2}", ConfigurationManager.AppSettings["downloads"], d.Type, d.Filename);

            return File(path, Util.GetContentType(path), System.IO.Path.GetFileName(path));
        }

    }
}
