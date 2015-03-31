using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbCore;
using DominosCMS.Web.Controllers;

namespace DominosCMS.Web.Areas.Admin.Controllers
{

    [Authorize(Roles = "admin")]

    public class AdminController<T> : BaseController where T : EntityBase
    {
        protected RepositoryBase<T> DataRepository { get; set; }

        public AdminController()
        {
            DataRepository = new RepositoryBase<T>(DB.Context);
        }
        
    }
}
