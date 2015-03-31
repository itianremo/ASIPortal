using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DominosCMS.Web.Helpers
{
    public static class ImageHelpers
    {
        public static MvcHtmlString ImageActionLink(this HtmlHelper helper, string imagePath, string altText, string url, object htmlAttributes)
        {
            TagBuilder a = new TagBuilder("a");

            a.MergeAttribute("href", url);
            a.MergeAttribute("title", altText);
            a.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            a.InnerHtml = Image(helper, null, imagePath, altText).ToString();

            return MvcHtmlString.Create(a.ToString());
        }

        public static MvcHtmlString Image(this HtmlHelper helper, string id, string url, string alternateText)
        {
            return Image(helper, id, url, alternateText, null);
        }

        public static MvcHtmlString Image(this HtmlHelper helper, string id, string url, string alternateText, object htmlAttributes)
        {
            // Create tag builder
            var builder = new TagBuilder("img");

            // Create valid id
            builder.GenerateId(id);

            // Add attributes
            builder.MergeAttribute("src", url);
            builder.MergeAttribute("alt", alternateText);
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            // Render tag
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

    }
}