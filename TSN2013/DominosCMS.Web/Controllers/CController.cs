using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DominosCMS.Web.Configuration;
using System.Configuration;
using System.Text;

namespace DominosCMS.Web.Controllers
{
    public class CController : Controller
    {
        //
        // GET: /C/

        public ActionResult test(int id, int id2)
        {
            return Content(string.Format("id = {0}, id2={1}", id, id2));
        }

        public ActionResult Index()
        {
            MenuTypesSettings config = (MenuTypesSettings)System.Configuration.ConfigurationManager.GetSection(MenuTypesConfigConstants.ConfigSectionName);

            var sb = new StringBuilder();

            for (int i = 0; i < config.MenuTypes.Count; i++)
			{
			    sb.Append(string.Format("{0}\t {1}\t {2}\t {3}\n", config.MenuTypes[i].Name, 
                                                                 config.MenuTypes[i].Title,
                                                                 config.MenuTypes[i].Controller,
                                                                 config.MenuTypes[i].Action
                                                                 ));

                for (int j = 0; j < config.MenuTypes[i].Parameters.Count; j++) {
                    sb.Append(string.Format("\tParam {0} ---> {1}: {2}\n", j, config.MenuTypes[i].Parameters[j].Name, config.MenuTypes[i].Parameters[j].Type));
                }
			}


            
            return Content(sb.ToString());
        }

    }
}
