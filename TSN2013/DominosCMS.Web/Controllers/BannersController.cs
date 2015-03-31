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

namespace DominosCMS.Web.Controllers
{
    public class BannersController : BaseController
    {
        public ActionResult Rotator()
        {
            var model = DB.BannerRepository.List.Where(b => b.ShowInHome).OrderBy(b => b.ViewOrder).ToList();
            return PartialView(model);
        }

    }
}

