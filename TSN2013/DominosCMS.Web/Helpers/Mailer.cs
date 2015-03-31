using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using System.Text;
using System.IO;

namespace DominosCMS.Web.Helpers
{
    internal static class Mailer
    {
        private static SmtpClient _smtp;

        static Mailer()
        {
            _smtp = new SmtpClient();
            _smtp.Host = ConfigurationManager.AppSettings["mailServer"];
        }

        internal static void Send(MailMessage msg)
        {
            Send(msg, true);
        }

        internal static void Send(MailMessage msg, bool sendAsHtml)
        {
            try
            {
                msg.From = new MailAddress(ConfigurationManager.AppSettings["emailsender"]);
                msg.IsBodyHtml = sendAsHtml;
                _smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["emailsender"], ConfigurationManager.AppSettings["emailPass"]);
                _smtp.Send(msg);
            }
            catch
            { }
        }


        internal static string BuildMessage(string filename, Dictionary<string, string> parameters)
        {
            StringBuilder sb = new StringBuilder(File.ReadAllText(filename));

            foreach (var item in parameters)
                sb.Replace(item.Key, item.Value);


            return sb.ToString();
        }
    }
}