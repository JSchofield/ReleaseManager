namespace ReleaseManager.Web.Models
{
    public class IssueViewModel
    {
        private readonly IIssue issue;

        public IssueViewModel(IIssue issue) { this.issue = issue; }

        public string Assignee { get { return this.issue.Assignee; } }

        public string Components { get { return this.issue.Components; } }

        public string Database { get { return this.issue.Database; } }

        public string Key { get { return this.issue.Key; } }

        public string Link { get { return this.issue.Link.ToString(); } }

        public string ReleaseStatus { get { return this.issue.CanBeReleased.ToString(); } }

        public string Status { get { return this.issue.Status; } }

        public string Summary { get { return this.issue.Summary; } }
    }
}