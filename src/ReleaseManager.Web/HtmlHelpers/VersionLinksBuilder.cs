namespace ReleaseManager.Web.HtmlHelpers
{
    using System;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using ReleaseManager.Web.Models;

    public static class VersionLinksBuilder
    {
        public static string VersionHeaderLinks(this HtmlHelper html, VersionViewModel version)
        {
            string currentAction = html.ViewContext.RouteData.Values["action"].ToString();
            TagBuilder builder = new TagBuilder("span");
            builder.MergeAttribute("class", "headerLinks");
            string[] strArray = new string[] { html.VersionLink(version, "All Commits", "Detail", currentAction), html.VersionLink(version, "WIP", "Wip", currentAction), html.VersionLink(version, "Tickets", "Tickets", currentAction) };
            builder.InnerHtml = string.Join("\n|\n", strArray);
            return builder.ToString();
        }

        public static string VersionLink(this HtmlHelper html, VersionViewModel version, string linkText, string action)
        {
            return html.VersionLink(version, linkText, action, null);
        }

        private static string VersionLink(this HtmlHelper html, VersionViewModel version, string linkText, string action, string currentAction)
        {
            if (!string.Equals(action, currentAction, StringComparison.InvariantCultureIgnoreCase))
            {
                return html.RouteLink(linkText, "Version", new { releaseName = version.ReleaseName, componentName = version.ComponentName, action = action }).ToString();
            }
            return MvcHtmlString.Create(linkText).ToString();
        }
    }
}