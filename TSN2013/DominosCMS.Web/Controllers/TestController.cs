using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DominosCMS.Web.Controllers
{
    public class TestController : BaseController
    {
        //
        // GET: /Admin/Test/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {

            var model = DB.AccountsRepository.List;

            return Json(model.Select(x => new
            {
                name = x.Username,
                email = x.Email,
                id = x.ID,
                code = x.ActivationCode
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

    }
}
