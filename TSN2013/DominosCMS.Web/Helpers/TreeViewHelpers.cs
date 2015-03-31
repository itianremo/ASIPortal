using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DominosCMS.Models;
using System.Web.Mvc;
using System.Text;
using System.Web.Routing;

namespace DominosCMS.Web.Helpers
{
    public static class TreeViewHelpers
    {
        private static UrlHelper url;

        static TreeViewHelpers()
        {
            HttpContextWrapper hcw = new HttpContextWrapper(HttpContext.Current); 

            RequestContext r = new RequestContext(hcw, new RouteData());
            url = new UrlHelper(r);
        }

        public static MvcHtmlString TreeView(this HtmlHelper helper, IEnumerable<MenuItem> menu, UrlHelper url)
        {
            TreeViewHelpers.url = url;
            TagBuilder builder = new TagBuilder("ul");

            builder.MergeAttribute("id", "tree");
            builder.MergeAttribute("id", "treeview");
            //we need to add root node

            TagBuilder li = new TagBuilder("li");

            li.MergeAttribute("class", "expandable");


            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("style", "font-weight: bold; color: red");
            a.MergeAttribute("href", url.Action("GetMenuItems", "SiteMenu"));
            a.SetInnerText("Root");
            li.InnerHtml += a.ToString();

            TagBuilder ul2 = new TagBuilder("ul");
            
            StringBuilder sb = new StringBuilder();

            foreach (var item in menu)
            {
                sb.Append(TreeNode(helper, item));
            }

            ul2.InnerHtml += sb.ToString();
            li.InnerHtml += ul2.ToString();
            builder.InnerHtml += li.ToString();

            return MvcHtmlString.Create(builder.ToString());
        }

        private static MvcHtmlString TreeNode(this HtmlHelper helper, MenuItem item)
        {
            TagBuilder builder = new TagBuilder("li");

            if (item.SubMenus != null && item.SubMenus.Count > 0)
            {
                builder.MergeAttribute("class", "expandable");

                //add the plus sign
                TagBuilder div = new TagBuilder("div");
                div.MergeAttribute("class", "hitarea expandable-hitarea");
                builder.InnerHtml = div.ToString();
                //////////////////////////////
            }

            //add the link
            TagBuilder a = new TagBuilder("a");

            a.MergeAttribute("href", url.Action("GetMenuItems", "SiteMenu", new { id = item.ID}));
            a.MergeAttribute("data-id", item.ID.ToString());
            a.SetInnerText(item.Title);
            builder.InnerHtml += a.ToString();

            //add sub menu
            if (item.SubMenus != null && item.SubMenus.Count > 0)
            {
                TagBuilder ul = new TagBuilder("ul");
                ul.MergeAttribute("style", "display: none");

                foreach (var m in item.SubMenus.OrderBy(x => x.ViewOrder))
                {
                    ul.InnerHtml += TreeNode(helper, m).ToHtmlString();
                }

                builder.InnerHtml += ul.ToString();
            }

            return MvcHtmlString.Create(builder.ToString());
        }
    }
}