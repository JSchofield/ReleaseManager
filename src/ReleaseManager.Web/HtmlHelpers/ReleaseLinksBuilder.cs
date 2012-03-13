namespace ReleaseManager.Web.HtmlHelpers
{
    using System;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class ReleaseLinksBuilder
    {
        public static string ReleaseHeaderLinks(this HtmlHelper html, string releaseName)
        {
            string currentAction = html.ViewContext.RouteData.Values["action"].ToString();

            string[] strArray = new [] {
                ReleaseLink(html, "Summary", releaseName, "Summary", currentAction),
                ReleaseLink(html, "WIP", releaseName, "WIP", currentAction), 
                ReleaseLink(html, "All Tickets", releaseName, "Tickets", currentAction), 
                ReleaseLink(html, "Edit", releaseName, "Edit", currentAction)};

            return  string.Join("\n|\n", strArray);
        }

        public static string ReleaseLink(this HtmlHelper html, string linkText, string releaseName, string action)
        {
            return ReleaseLink(html, linkText, releaseName, action, null);
        }

        private static string ReleaseLink(HtmlHelper html, string linkText, string releaseName, string action, string currentAction)
        {
            if (!string.Equals(action, currentAction, StringComparison.InvariantCultureIgnoreCase))
            {
                return html.RouteLink(linkText, "Release", new { releaseName, action }).ToString();
            }
            return MvcHtmlString.Create(linkText).ToString();
        }
    }
}