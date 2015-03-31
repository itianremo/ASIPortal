using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DominosCMS.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Search", // Route name
                "Search/{kw}", // URL with parameters
                new { controller = "Home", action = "Search", kw = UrlParameter.Optional }, // Parameter defaults
                new[] { "DominosCMS.Web.Controllers" }
            );
            routes.MapRoute(
                "ProductsDownloads", // Route name
                "Products/Download/{id}", // URL with parameters
                new { controller = "ProductsViewer", action = "Download", UrlParameter.Optional }, // Parameter defaults
                new[] { "DominosCMS.Web.Controllers" }
            );
            routes.MapRoute(
                "Products", // Route name
                "Products/{id}", // URL with parameters
                new { controller = "ProductsViewer", action = "Index", id = 1 }, // Parameter defaults
                new[] { "DominosCMS.Web.Controllers" }
            );
            routes.MapRoute(
                "Product", // Route name
                "Product/{id}", // URL with parameters
                new { controller = "ProductsViewer", action = "Index", id = 1 }, // Parameter defaults
                new[] { "DominosCMS.Web.Controllers" }
            );
            routes.MapRoute(
                "LSFApp", // Route name
                "LSFApp", // URL with parameters
                new { controller = "Home", action = "LSFApp", id = "" }, // Parameter defaults
                new[] { "DominosCMS.Web.Controllers" }
            );
            routes.MapRoute(
                "FireRated", // Route name
                "FireRated", // URL with parameters
                new { controller = "Home", action = "FireRated", id = "" }, // Parameter defaults
                new[] { "DominosCMS.Web.Controllers" }
            );
            routes.MapRoute(
                "FRList", // Route name
                "FRList", // URL with parameters
                new { controller = "Home", action = "FRList", id = "" }, // Parameter defaults
                new[] { "DominosCMS.Web.Controllers" }
            ); routes.MapRoute(
               "contact", // Route name
               "Contact", // URL with parameters
               new { controller = "Home", action = "Contact", url = UrlParameter.Optional }, // Parameter defaults
               new[] { "DominosCMS.Web.Controllers" }
           );
            routes.MapRoute(
               "about", // Route name
               "about", // URL with parameters
               new { controller = "Home", action = "about", url = UrlParameter.Optional }, // Parameter defaults
               new[] { "DominosCMS.Web.Controllers" }
           );
            routes.MapRoute(
               "optout", // Route name
               "optout", // URL with parameters
               new { controller = "Home", action = "OptOut", url = UrlParameter.Optional }, // Parameter defaults
               new[] { "DominosCMS.Web.Controllers" }
           );
            routes.MapRoute(
                "downloads", // Route name
                "Page/Download/{id}", // URL with parameters
                new { controller = "Downloads", action = "File", id = UrlParameter.Optional }, // Parameter defaults
                new[] { "DominosCMS.Web.Controllers" }

           );
            routes.MapRoute(
                "Pages", // Route name
                "site/{url}", // URL with parameters
                new { controller = "Pages", action = "Details", url = UrlParameter.Optional }, // Parameter defaults
                new[] { "DominosCMS.Web.Controllers" }

           );
            //routes.MapRoute(
            //    "Default2", // Route name
            //    "{controller}/{action}/{id}-{SelectNo}", // URL with parameters
            //    new { controller = "Home", action = "Index", id = "0", SelectNo = "0" }, // Parameter defaults
            //    new[] { "DominosCMS.Web.Controllers" }

            //);
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new[] { "DominosCMS.Web.Controllers" }

            );

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
           
            if (this.Context.Request.IsAuthenticated)
            {
                // Auth. VIA User Domain Name // Added by Fawzi
                //string userName = Environment.UserName;
                //string[] roles = getRoles(userName);
                //////////////////////////////////////////////////////////////
                // Original Auth. Code
                string[] roles = getRoles(this.Context.User.Identity.Name);
                //////////////////////////////////////////////////////////////
                this.Context.User = new System.Security.Principal.GenericPrincipal(this.Context.User.Identity, roles);
            }
            

        }

        private string[] getRoles(string username)
        {
            using (var db = new DominosCMS.Repositories.UnitOfWork())
            {
                var account = db.AccountsRepository.List.Where(a => a.Username == username).Single();

                string[] roles = account.Roles.Split(new char[] {','});

                return roles;
            }
        }


        protected void Application_Start()
        {
            Database.SetInitializer<Repositories.CMSDbContext>(null);
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            EO.Pdf.Runtime.AddLicense( 
                "WlmXpLHLn1mXwPIP41nr/QEQvFu807/745+ZpAcQ8azg8//ooW2ltLPLrneE" + 
                "jrHLn1mzs/IX66juwp61n1mXpM0a8Z3c9toZ5aiX6PIf5HaZt8DdtGiptMLe" + 
                "oVnt6QMe6KjlwbPfoVmmwp61n1mXpM0e6KDl5QUg8Z618OLk84Tv+ODWz7De" + 
                "0sYb7Wy9+dkAwHa0wMAe6KDl5QUg8Z61kZvnrqXg5/YZ8p61kZt14+30EO2s" + 
                "3MKetZ9Zl6TNF+ic3PIEEMidtbnC3K9xqrTH3LZ1pvD6DuSn6unaD71GgaSx" + 
                "y5914+30EO2s3OnP566l4Of2GfKe3MKetZ9Zl6TNDOul5vvPuIk=");

        }

        public void Application_Error(object sender, EventArgs e)
        {
            //HttpContext con = HttpContext.Current;
            //string fileName = con.Request.Url.ToString();


            //Exception exception = Server.GetLastError();
            //Response.Clear();

            //HttpException httpException = exception as HttpException;

            //if (httpException != null)
            //{
            //    if (fileName.ToLower().Contains(".pdf") && fileName.ToLower().Contains("/content/res/pdf/") && !fileName.ToLower().Contains("/data/files/content/res/pdf/"))
            //    {
            //        HttpContext.Current.Server.ClearError();
            //        fileName = fileName.Replace("/content/res/pdf/", "/data/files/content/res/pdf/");
            //        Response.ContentType = "application/pdf";
            //        HttpContext.Current.Response.Redirect(fileName, true);
            //    }

            //    if (fileName.ToLower().Contains("http:/") && !fileName.ToLower().Contains("http://"))
            //    {
            //        HttpContext.Current.Server.ClearError();
            //        fileName = fileName.Replace("http:/", "http://");
            //        HttpContext.Current.Response.Redirect(fileName, true);
            //    }


            //}


        }

    }
}