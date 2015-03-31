using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosCMS.Repositories;

namespace DominosCMS.Web.Controllers
{
    public class BaseController : Controller
    {
        private UnitOfWork _db;

        protected UnitOfWork DB {
            get 
            {
                if (_db == null)
                    _db = new UnitOfWork();

                return _db;
            }
        }

        public BaseController()
        {
            
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            
            if (_db != null)
                _db.Dispose();
        }

    }
}
