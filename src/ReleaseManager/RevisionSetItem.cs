using System;

namespace ReleaseManager
{
    public class RevisionSetItem : IRevisionSetItem
    {
        private readonly IRevision revision;

        public RevisionSetItem(IRevision revision, bool canBeReleased)
        {
            this.revision = revision;
            this.CanBeReleased = revision.ReleaseStatus && canBeReleased;
            this.IsBlocked = revision.ReleaseStatus && !canBeReleased;
        }

        public bool CanBeReleased { get; private set; }

        public bool IsBlocked { get; private set; }

        public string Author { get { return this.revision.Author; } }

        public IIssueCollection Issues { get { return this.revision.Issues; } }

        public string Message { get { return this.revision.Message; } }

        public bool NaturalReleaseStatus { get { return this.revision.NaturalReleaseStatus; } }

        public long Number { get { return this.revision.Number; } }

        public bool ReleaseStatus { get { return this.revision.ReleaseStatus; } }

        public DateTime Time { get { return this.revision.Time; } }

        public IRevisionOverride UserOverride
        {
            get
            {
                return this.revision.UserOverride;
            }
            set
            {
                this.revision.UserOverride = value;
            }
        }

        public bool Ignore { get { return this.revision.Ignore; } }

        public bool NaturalIgnore { get { return this.revision.NaturalIgnore; } }
    }
}