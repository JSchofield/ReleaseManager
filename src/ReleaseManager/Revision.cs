using System;

namespace ReleaseManager
{
    public class Revision : IRevision
    {
        private readonly ILogItem logItem;

        public Revision(ILogItem logItem, IRevisionOverride userOverride, IIssueCollection issues)
        {
            this.logItem = logItem;
            this.Issues = issues;
            this.UserOverride = userOverride;
        }

        public long Number { get { return this.logItem.Revision; } }

        public string Author { get { return this.logItem.Author; } }

        public DateTime Time { get { return this.logItem.Time; } }

        public string Message { get { return this.logItem.Message; } }

        public IIssueCollection Issues { get; private set; }

        public bool NaturalReleaseStatus
        {
            get
            {
                if (this.logItem.Directives.Contains("ignore"))
                {
                    return true;
                }
                if (this.Issues.Count == 0)
                {
                    return false;
                }
                return this.Issues.CanBeReleased;
            }
        }

        public bool NaturalIgnore { get { return this.logItem.Directives.Contains("ignore"); } }

        public IRevisionOverride UserOverride { get; set; }

        public bool Ignore
        {
            get
            {
                return
                    this.logItem.Directives.Contains("ignore")
                    || (this.UserOverride != null
                        && this.UserOverride.Ignore);
            }
        }

        public bool ReleaseStatus
        {
            get
            {
                if (this.UserOverride != null)
                {
                    return this.UserOverride.CanBeReleased;
                }
                return this.NaturalReleaseStatus;
            }
        }
    }
}