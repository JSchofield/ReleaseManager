namespace ReleaseManager.Web.HtmlHelpers
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;
    using ReleaseManager.Web.Models;

    public static class ReleaseStatusGraphBuilder
    {
        private static string BuildCell(double totalRevisions, double selectedRevisions, string cssClass, string type)
        {
            int num = (int) Math.Round((100.0 * (selectedRevisions / totalRevisions)), 0);
            if (num == 0)
            {
                return string.Empty;
            }
            TagBuilder builder = new TagBuilder("td");
            builder.Attributes.Add("class", cssClass);
            builder.Attributes.Add("title", string.Format(CultureInfo.CurrentCulture, "{0} {1} commits", selectedRevisions, type));
            builder.Attributes.Add("style", string.Format(CultureInfo.CurrentCulture, "width:{0}%", num));
            return builder.ToString();
        }

        public static string ReleaseStatusGraph(this HtmlHelper html, VersionViewModel version)
        {
            if (int.Parse(version.TotalCommits, CultureInfo.InvariantCulture) == 0)
            {
                return MvcHtmlString.Create(string.Empty).ToString();
            }
            TagBuilder builder = new TagBuilder("table");
            builder.Attributes.Add("class", "graph");
            TagBuilder builder2 = new TagBuilder("tr");
            var strArray = new [] {
                    BuildCell(int.Parse(version.TotalCommits, CultureInfo.InvariantCulture), version.ReadyRevisions, "graph ready", "Ready"), 
                    BuildCell(int.Parse(version.TotalCommits, CultureInfo.InvariantCulture), version.IgnoreRevisions, "graph ignore", "Ignore"),
                    BuildCell(int.Parse(version.TotalCommits, CultureInfo.InvariantCulture), version.NotReadyRevisions, "graph notReady", "NotReady"),
                    BuildCell(int.Parse(version.TotalCommits, CultureInfo.InvariantCulture), version.BlockedRevisions, "graph blocked", "Blocked")};
            builder2.InnerHtml = string.Join("\n", strArray);
            builder.InnerHtml = builder2.ToString();
            return builder.ToString();
        }
    }
}