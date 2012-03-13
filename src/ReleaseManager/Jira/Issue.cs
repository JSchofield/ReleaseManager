using System;

namespace ReleaseManager.Jira
{
    public class Issue : IIssue
    {
        public string Assignee{get; set; }
        public string Components{get; set; }
        public string Database{get; set; }
        public string Key{get; set; }
        public Uri Link{get; set; }
        public bool CanBeReleased{get; set; }
        public string Status{get; set; }
        public string Summary{get; set; }
    }
}