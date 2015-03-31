using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using DominosCMS.Models;
using DominosCMS.Repositories;
using DominosCMS.Web.Models;
using DominosCMS.Web.Helpers;
using System.Runtime.InteropServices;


namespace DominosCMS.Web.Controllers
{
    public class AccountController : BaseController
    {
        [DllImport("ADVAPI32.dll", EntryPoint =
        "LogonUserW", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool LogonUser(string lpszUsername,
        string lpszDomain, string lpszPassword, int dwLogonType,
        int dwLogonProvider, ref IntPtr phToken);
        //
        // GET: /Account/LogOn
        public ActionResult LogOn()
        {
            LogOnModel model= new LogOnModel();
            return View(model);
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        public ActionResult LoginUser(string username, string password)
        {
                if (ValidateUser(username, password))
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    return View("LogOn");
                }

        }


        private bool ValidateUser(string username, string password)
        {
            ///////////////////////////////////////////////////////////////
            //Get UserName and domain from Domain // Windows Auth. // Added By Fawzi
            //string userName = Environment.UserName;
            string domainName = Environment.UserDomainName;
            IntPtr token = IntPtr.Zero;
            // check user in domain controller//
            bool result = LogonUser(username, domainName, password, 2, 0, ref token);
            return result;
            ///////////////////////////////////////////////////////////////
            // Original Auth. Code
            //int count = 0;
            //count = DB.AccountsRepository.List.Where(acc => acc.Username == username && acc.Password == password).Count();
            //return count > 0;
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            HttpCookie myCookie = new HttpCookie("returnAccount");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ExitImpersonation()
        {
            FormsAuthentication.SignOut();

            if (Request.Cookies["returnAccount"] != null)
            {
                FormsAuthentication.SetAuthCookie(Request.Cookies["returnAccount"].Value, false);

                HttpCookie myCookie = new HttpCookie("returnAccount");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }

            return RedirectToAction("Index", "Members", new { area = "Admin"});
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            RegistrationModel model = new RegistrationModel();

            return View(model);
        }

        //
        // POST: /Account/Register


        [HttpPost]
        public ActionResult Register(RegistrationModel model, HttpPostedFileBase company_logo, string OtherType)
        {
            if (!model.AcceptTerms)
                ModelState.AddModelError("", "You must accept our terms and conditions to continue");

            if (isEmailRegistred(model.Email))
                ModelState.AddModelError("Email", "This email is already registered");

            if (company_logo != null)
            {
                if (company_logo.ContentLength > 100 * 1024)
                    ModelState.AddModelError("Company.Logo", "File size is very big");

                else if (!Util.IsImage(company_logo.ContentType))
                    ModelState.AddModelError("Company.Logo", "File type is not valid");
            }

            if (ModelState.IsValid)
            {
                if (company_logo != null)
                {
                    var logo = string.Format("{0}-{1}", DateTime.Now.Ticks, Path.GetExtension(company_logo.FileName));

                    string path = string.Format("{0}/{1}", ConfigurationManager.AppSettings["CompanyLogos"], logo);

                    path = Server.MapPath(path);
                    company_logo.SaveAs(path);
                    //model.Company.Logo = logo;
                }

                // Attempt to register the user
                Account acc = new Account();
                ///////////////////////////
                // Added By Fawzi // To register User Name on the Domain //
                acc.Username = Environment.UserName;
                ///////////////////////////
                //acc.Username = model.Email;
                //
                acc.Email = model.Email;
                acc.Password = model.Password;
                acc.Roles = "user";
                acc.Activated = false;
                acc.ActivationCode = Guid.NewGuid().ToString();

                //acc.Company = model.Company;

                //acc.Company.Type = (model.Company.Type == "Other" ? OtherType : model.Company.Type);

                DB.AccountsRepository.Add(acc);

                DB.SaveChanges();

                //Send Activation Email 
                Dictionary<string, string> data = new Dictionary<string, string>();

                //data.Add("{NAME}", string.Format("{0} {1}", acc.Company.FirstName, acc.Company.LastName));
                var url = string.Format("http://{0}:{1}{2}", Request.Url.Host, Request.Url.Port, Url.Action("ActivateAccount", new { id = acc.ID, v = acc.ActivationCode }));

                data.Add("{URL}", url);


                string filename = Server.MapPath("~/_static/mails/ActivationTemplate.html");

                string msg = Helpers.Util.BuildEmailFromTemplate(filename, data);

                System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
                mailMsg.Body = msg;
                mailMsg.Subject = "The Steel Network Account Activation";
                mailMsg.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["emailsender"].ToString());
                mailMsg.To.Add(model.Email);
                mailMsg.IsBodyHtml = true;

                //return Content(msg);

                Helpers.Mailer.Send(mailMsg);

                return View("VerificationSent");
            }

            return View(model);
        }

        public ActionResult ActivateAccount(int id, string v)
        {
            var account = DB.AccountsRepository.FindBy(id);

            if (account != null)
            {
                if (account.ActivationCode.Equals(v))
                {
                    account.Activated = true;

                    DB.SaveChanges();

                    return View("ActivationSuccess");
                }
            }

            return View("ActivationFailed");
        }

        private bool isEmailRegistred(string username)
        {
            int count = DB.AccountsRepository.List.Where(a => a.Username == username).Count();

            return count > 0;
            
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                        var account = DB.AccountsRepository.List.Where(a => a.Username == User.Identity.Name).SingleOrDefault();
                        if (account.Password == model.OldPassword)
                        {
                            account.Password = model.NewPassword;
                            DB.SaveChanges();
                            changePasswordSucceeded = true;
                        }
                        else
                            changePasswordSucceeded = false;


                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "Password change was unsuccessful.  Please try again.");
                }
            }


            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult ForgotPassword(string email)
        //{
        //    var acc  = DB.AccountsRepository.List.Where(a => a.Email == email).SingleOrDefault();

        //    if (acc == null)
        //    {
        //        ModelState.AddModelError("email", "The email you provided is not registered, please try again");
        //        return View();
        //    }

        //    string filename = "~/_static/mails/forgotpass.html";
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    parameters.Add("{NAME}", acc.Company.FirstName);
        //    parameters.Add("{USERNAME}", acc.Username);
        //    parameters.Add("{PASSWORD}", acc.Password);

        //    string mailMessage = Helpers.Mailer.BuildMessage(Server.MapPath(filename), parameters);

        //    MailMessage emailer = new MailMessage();
        //    emailer.To.Add(acc.Email);
        //    //email.To.Add("hebakr@gmail.com");
        //    //emailer.ReplyToList.Add(new MailAddress(acc.Email, acc.Company.FirstName));
        //    emailer.Subject = "The Steel Network Password Retrieval";
        //    emailer.Body = mailMessage;

        //    Helpers.Mailer.Send(emailer);



        //    return View("PasswordSent");
        //}


        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
