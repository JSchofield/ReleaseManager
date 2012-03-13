namespace ReleaseManager.Web.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class EmptyRevisionViewModel : RevisionViewModel
    {
        public override string Author { get { return string.Empty; } }

        public override bool IsEmpty { get { return true; } }

        public override IEnumerable<IssueViewModel> Issues { get { return new Collection<IssueViewModel>(); } }

        public override string Message { get { return string.Empty; } }

        public override ReleaseStatusViewModel NaturalReleaseStatus { get { return null; } }

        public override long Number { get { return 0; } }

        public override ReleaseStatusViewModel PropogatedStatus { get { return null; } }

        public override ReleaseStatusViewModel ReleaseStatus { get { return null; } }

        public override string Time { get { return string.Empty; } }

        public override ReleaseStatusViewModel UserOverride { get { return null; }  }
    }
}