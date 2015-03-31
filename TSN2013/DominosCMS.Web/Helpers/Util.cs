using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using DominosCMS.Models;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace DominosCMS.Web.Helpers
{
    public static class Util
    {

        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        public static SelectList GetAccountCompanySelectList(string CompanyTypeStr)
        {
            CompanyTypeStr = (CompanyTypeStr.Contains("|")) ? CompanyTypeStr.Remove(CompanyTypeStr.IndexOf("|")) : CompanyTypeStr;
            return new SelectList(new[] { "Architect", "Contractor", "Distributor", "Engineer", "Other" }
                .Select(x => new { value = x, text = x }),
                "value", "text", CompanyTypeStr);
        }

        public static string GetAccountCompanyType(string CompanyTypeStr)
        {
            if (CompanyTypeStr.Contains("|"))
                return CompanyTypeStr.Remove(CompanyTypeStr.IndexOf("|"));

            return CompanyTypeStr;
        }

        public static string GetAccountCompanyOtherType(string CompanyTypeStr)
        {
            string strRegex = @"(\w+)??[?|]?(\w+)";
            RegexOptions myRegexOptions = RegexOptions.None;
            Regex myRegex = new Regex(strRegex, myRegexOptions);
            MatchCollection myMatchs = myRegex.Matches(CompanyTypeStr);
            if (myMatchs.Count > 1)
                return myMatchs[1].Value.Remove(0,1);
            else
                return "";
        }
        
        public static string GetAccountCompanyTypeView(string CompanyTypeStr)
        {
            string strRegex = @"(\w+)??[?|]?(\w+)";
            RegexOptions myRegexOptions = RegexOptions.None;
            Regex myRegex = new Regex(strRegex, myRegexOptions);
            MatchCollection myMatchs = myRegex.Matches(CompanyTypeStr);
            if (myMatchs.Count > 1)
                return myMatchs[1].Value.Remove(0, 1);
            else
                return myMatchs[0].Value;
        }

        public static string GetAccountPhoneEx1(string PhoneStr)
        {
            if (string.IsNullOrEmpty(PhoneStr)) return string.Empty;
            string strRegex = @"(\d+)??[?|]?(\d+)";
            RegexOptions myRegexOptions = RegexOptions.None;
            Regex myRegex = new Regex(strRegex, myRegexOptions);
            MatchCollection myMatchs = myRegex.Matches(PhoneStr);
            if (myMatchs.Count > 1)
                return myMatchs[0].Value;
            else
                return "";
        }

        public static string GetAccountPhoneEx2(string PhoneStr)
        {
            if (string.IsNullOrEmpty(PhoneStr)) return string.Empty;
            string strRegex = @"(\d+)??[?|]?(\d+)";
            RegexOptions myRegexOptions = RegexOptions.None;
            Regex myRegex = new Regex(strRegex, myRegexOptions);
            MatchCollection myMatchs = myRegex.Matches(PhoneStr);
            if (myMatchs.Count > 2)
                return myMatchs[1].Value.Remove(0, 1);
            else
                return "";
        }

        public static string GetAccountPhone(string PhoneStr)
        {
            if (string.IsNullOrEmpty(PhoneStr)) return string.Empty;
            string strRegex = @"(\d+)??[?|]?(\d+)";
            RegexOptions myRegexOptions = RegexOptions.None;
            Regex myRegex = new Regex(strRegex, myRegexOptions);
            MatchCollection myMatchs = myRegex.Matches(PhoneStr);
            if (myMatchs.Count == 1)
                return myMatchs[0].Value;
            else if (myMatchs.Count == 2)
                return myMatchs[1].Value.Remove(0, 1);
            else if (myMatchs.Count == 3)
                return myMatchs[2].Value.Remove(0, 1);
            else
                return "";
        }

        public static string GetAccountPhoneView(string PhoneStr)
        {
            if (string.IsNullOrEmpty(PhoneStr)) return string.Empty;

            return PhoneStr.Replace("|", " ");
        }

        internal static string BuildEmailFromTemplate(string filename, Dictionary<string, string> parameters)
        {
            StreamReader reader = File.OpenText(filename);
            string line = "";
            System.Text.StringBuilder mailText = new System.Text.StringBuilder(); ;
            while ((line = reader.ReadLine()) != null)
                mailText.Append(line);

            reader.Close();

            foreach (var item in parameters)
            {
                mailText.Replace(item.Key, item.Value);
                
            }

            return mailText.ToString();
        }

        public static bool HasContents(string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;
            //return true if the text has image tag
            if (text.Contains("<img "))
                return true;

            //remove the tags
            string cleanText = _htmlRegex.Replace(text, string.Empty);

            return !string.IsNullOrWhiteSpace(cleanText);
        }
        
        public static string GetContentType(string path)
        {
            string ext = System.IO.Path.GetExtension(path).Remove(0, 1);

            string contentType = "application/octet-stream";
            
            switch (ext)
            {
                case "asf":
                    contentType = "video/x-ms-asf";
                    break;
                case "avi":
                    contentType = "video/avi";
                    break;
                case "doc":
                    contentType = "application/msword";
                    break;
                case "zip":
                    contentType = "application/zip";
                    break;
                case "xls":
                    contentType = "application/vnd.ms-excel";
                    break;
                case "gif":
                    contentType = "image/gif";
                    break;
                case "jpg":
                case "jpeg":
                    contentType = "image/jpeg";
                    break;
                case "png":
                    contentType = "image/png";
                    break;
                case "wav":
                    contentType = "audio/wav";
                    break;
                case "mp3":
                    contentType = "audio/mpeg3";
                    break;
                case "mpg":
                case "mpeg":
                    contentType = "video/mpeg";
                    break;
                case "rtf":
                    contentType = "application/rtf";
                    break;
                case "dwg":
                    contentType = "application/acad";
                    break;
                case "pdf":
                    contentType = "application/pdf";
                    break;

            }

            return contentType;
        }

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }

        public static MvcHtmlString NewLineToHtmlBreak(string text)
        {
            return MvcHtmlString.Create(text.Replace("\r\n", "<br />"));
        }

        public static bool IsImage(string mimeType) 
        {
            string[] mimeTypes = new string[] { "image/gif", "image/jpeg", "image/png", "image/pjpeg", "image/x-png" };

            return mimeTypes.Contains(mimeType);
        }

        internal static string GetFileContent(string file)
        {
            StreamReader reader = File.OpenText(file);
            string line = "";
            System.Text.StringBuilder mailText = new System.Text.StringBuilder(); ;
            while ((line = reader.ReadLine()) != null)
                mailText.Append(line);

            reader.Close();


            return mailText.ToString();
        }

        public static MvcHtmlString SuperScript(string text)
        {
            return MvcHtmlString.Create(text.Replace("™", "<sup>™</sup>").Replace("®", "<sup>®</sup>").Replace("&reg;", "<sup>&reg;</sup>"));

        }


        public static string GetProductType(string p)
        {
            if (!string.IsNullOrEmpty(p) && p.Equals("m"))
                return "Member";
            if (!string.IsNullOrEmpty(p) && p.Equals("c"))
                return "Connection";

            return "Other";
        }
    }
}