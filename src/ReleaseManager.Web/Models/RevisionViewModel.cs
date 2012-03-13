namespace ReleaseManager.Web.Models
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;

    public class RevisionViewModel
    {
        private readonly IRevisionSetItem revision;

        public RevisionViewModel()
        {}

        public RevisionViewModel(IRevisionSetItem revision): this(revision, false, false)
        {}

        public RevisionViewModel(IRevisionSetItem revision, bool selected, bool blocking)
        {
            this.revision = revision;
            this.Number = revision.Number;
            this.Selected = selected;
            this.Blocking = blocking;
            if (revision.UserOverride != null)
            {
                this.UserOverride =
                    new ReleaseStatusViewModel
                    {
                        CanBeReleased = revision.UserOverride.CanBeReleased,
                        Ignore = revision.UserOverride.Ignore
                    };
            }
            else
            {
                this.UserOverride = new ReleaseStatusViewModel();
            }

            this.NaturalReleaseStatus =
                    new ReleaseStatusViewModel
                    {
                        CanBeReleased = revision.NaturalReleaseStatus,
                        Ignore = revision.NaturalIgnore
                    };

            this.ReleaseStatus =
                    new ReleaseStatusViewModel
                    {
                        CanBeReleased = revision.ReleaseStatus,
                        Ignore = revision.Ignore
                    };

            this.PropogatedStatus =
                    new ReleaseStatusViewModel
                    {
                        CanBeReleased = revision.CanBeReleased,
                        Ignore = revision.Ignore,
                        IsBlocked = revision.IsBlocked
                    };
        }

        public virtual bool CanBeReleased { get { return this.revision.CanBeReleased; } }

        public virtual bool Ignore { get { return this.revision.Ignore; } }

        public virtual string Author { get { return this.revision.Author; } }

        public virtual bool IsEmpty { get { return false; } }

        public virtual IEnumerable<IssueViewModel> Issues
        {
            get
            {
                return this.revision.Issues.Select(i => new IssueViewModel(i));
            }
        }

        public virtual string Message
        {
            get
            {
                string str = string.IsNullOrEmpty(this.revision.Message) ? "No commit message" : this.revision.Message;
                return string.Format(CultureInfo.CurrentCulture, "Revision {0}: {1}", this.Number, str);
            }
        }

        public virtual ReleaseStatusViewModel NaturalReleaseStatus { get; private set; }

        public bool Selected { get; set; }
        public bool Blocking { get; set; }

        [HiddenInput]
        public virtual long Number { get; private set; }

        public virtual ReleaseStatusViewModel PropogatedStatus { get; private set; }

        public virtual ReleaseStatusViewModel ReleaseStatus { get; private set; }

        public virtual string Time
        {
            get
            {
                return (this.revision.Time.ToShortDateString() + " " + this.revision.Time.ToShortTimeString());
            }
        }

        public virtual ReleaseStatusViewModel UserOverride { get; private set; }
    }
}