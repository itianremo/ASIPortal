using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosCMS.Models;

namespace DominosCMS.Web.Areas.Admin.Controllers
{
    public class ManageUsersController : AdminController<Account>
    {
        //
        // GET: /Admin/ManageUsers/

        public ActionResult Index()
        {
            return View(DB.AccountsRepository.FindAll());
           
        }

    }
}
