using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Web.Routing;
using DominosCMS.Models;
using System.Configuration;

namespace DominosCMS.Web.Helpers
{
    public static class HtmlHelpers
    {

        public static MvcHtmlString ClipLink(this HtmlHelper helper, string url, string removeUrl, int[] submittalItems, int productID)
        {
            var builder = new TagBuilder("a");

            // Add attributes
            builder.MergeAttribute("href", url);
            builder.MergeAttribute("data-Remove-Action", removeUrl);
            builder.AddCssClass("action");

            if (submittalItems.Contains(productID))
            {
                builder.AddCssClass("delete-section");
                builder.SetInnerText("Remove");
                builder.MergeAttribute("title", "Click to remove from submittal");
            }
            else
            {
                builder.MergeAttribute("title", "Click to Add to submittal");
                builder.SetInnerText("Add");

            }

            // Render tag
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString SubmittalLink(this HtmlHelper helper, string url, string removeUrl, int[] submittalItems, int sectionID) 
        {
            var builder = new TagBuilder("a");

            // Add attributes
            builder.MergeAttribute("href", url);
            builder.MergeAttribute("data-Remove-Action", removeUrl);
            builder.AddCssClass("action");

            if (submittalItems.Contains(sectionID))
            {
                builder.AddCssClass("delete-section");
                builder.MergeAttribute("title", "Click to remove from submittal");
                builder.SetInnerText("Remove");
            }
            else
            { 
                builder.MergeAttribute("title", "Click to Add to submittal");
                builder.SetInnerText("Add");
            }




            // Render tag
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString Script(this HtmlHelper helper, string url, bool absolutePath = false)
        {
            var scriptFolder = ConfigurationManager.AppSettings["ScriptsFolder"]; 
            var builder = new TagBuilder("script");
            builder.MergeAttribute("type", "text/javascript");
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            string path = (absolutePath) ? urlHelper.Content(url) : string.Format("{0}{1}", scriptFolder, url);
            builder.MergeAttribute("src", path);

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));

        }

        public static MvcHtmlString Css(this HtmlHelper helper, string url, bool absolutePath = false)
        {
            var stylesFolder = ConfigurationManager.AppSettings["StylesFolder"];
            var builder = new TagBuilder("link");
            builder.MergeAttribute("type", "text/css");
            builder.MergeAttribute("rel", "stylesheet");
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            string path = (absolutePath) ? urlHelper.Content(url) : string.Format("{0}{1}", stylesFolder, url);
            builder.MergeAttribute("href", path);

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));

        }


    }
}