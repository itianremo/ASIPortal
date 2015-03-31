using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using DominosCMS.Models;
using DominosCMS.Repositories;
using DominosCMS.Web.Models;

namespace DominosCMS.Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var model = DB.PageRepository.FindBy("home");

            return View(model);
        }


        public ActionResult Contact()
        {
            Page page = DB.PageRepository.FindBy("contact");

            ContactModel model = new ContactModel();
            model.PageContents = page.Contents;
            model.PageTitle = page.Title;

            FillCountries(model.Countries);
            FillStates(model.States);

            return View(model);

            //return View();
        }

        private void FillStates(Dictionary<string, string> states)
        {
            DataSet ds = new DataSet();

            //add states to the model
            ds.ReadXml(Server.MapPath("~/App_Data/States.xml"));
            states.Add("", "Select");
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                states.Add(r[1].ToString(), r[1].ToString());
            }
        }

        private void FillCountries(Dictionary<string, string> countries)
        {
            DataSet ds = new DataSet();

            //add states to the model
            ds.ReadXml(Server.MapPath("~/App_Data/Countries.xml"));
            countries.Add("", "Select country");
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                countries.Add(r[1].ToString(), r[1].ToString());
            }
        }



        public ActionResult OptOut()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OptOut(OptOutRequest model)
        {
            if (ModelState.IsValid)
            {
                model.RequestDate = DateTime.Now;

                DB.OptOutRepository.Add(model);

                DB.SaveChanges();

                return View("OptOutRequestSubmitted");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel model, string myCaptcha)
        {
            if (!Helpers.CaptchaHelper.VerifyAndExpireSolution(HttpContext, myCaptcha, model.Attempt))
            {
                // Redisplay the view with an error message
                ModelState.AddModelError("Attempt", "Incorrect - please try again");
            }


            if (ModelState.IsValid)
            {
                //Send the email

                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("{FirstName}", model.FirstName);
                data.Add("{LastName}", model.LastName);
                data.Add("{Phone}", model.Phone);
                data.Add("{Email}", model.Email);
                data.Add("{Company}", model.Company);
                data.Add("{Country}", model.Country);
                data.Add("{State}", model.State);
                data.Add("{OS}", model.OperatingSystem);
                data.Add("{HW}", model.Hardware);
                data.Add("{Subject}", model.Subject);
                data.Add("{Comments}", model.QueryDetails);

                string filename = Server.MapPath("~/_static/mails/ContactTemplate.htm");

                string msg = Helpers.Util.BuildEmailFromTemplate(filename, data); 


                //return Content(msg);

                MailMessage email = new MailMessage();
                email.To.Add("support@steelnetwork.com");
                email.From = new System.Net.Mail.MailAddress(model.Email);
                email.Body = msg;
                email.Subject = string.Format("New inquiry from {0} {1}", model.FirstName, model.LastName);
                email.IsBodyHtml = true;
                Helpers.Mailer.Send(email);
                //Show thank you view
                return View("ContactSubmitted");
            }

            Page page = DB.PageRepository.FindBy("contact");

            model.PageContents = page.Contents;
            model.PageTitle = page.Title;

            FillCountries(model.Countries);
            FillStates(model.States);


            return View(model);
        }

        public ActionResult NotFound(string aspxerrorpath)
        {
            
            //if (!string.IsNullOrEmpty(aspxerrorpath))
            //{
            //    if (aspxerrorpath.StartsWith("/content/", StringComparison.OrdinalIgnoreCase))
            //        ViewBag.Url = Url.Content(string.Format("~/data/files{0}", aspxerrorpath));
            //}
            //else
            //{
            //    if (Request.RawUrl.StartsWith("/content/", StringComparison.OrdinalIgnoreCase))
            //        ViewBag.Url = Url.Content(string.Format("~/data/files{0}", Request.RawUrl));

            //}

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }


        [Authorize(Roles="admin")]
        public ActionResult Admin() 
        {
            return View();
        }



    }
}
