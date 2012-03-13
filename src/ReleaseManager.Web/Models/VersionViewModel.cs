namespace ReleaseManager.Web.Models
{
    using System.Globalization;
    using System.Linq;
    using System.Collections.Generic;

    public class VersionViewModel
    {
        private readonly IVersionWork version;

        public VersionViewModel()
        {
            this.Revisions = new List<RevisionViewModel>();
        }

        public VersionViewModel(IVersionWork version)
        {
            this.version = version;
            this.ComponentName = version.Component.Name;
            this.ReleaseName = version.Release.Name;
            this.SelectedRevision = version.SelectedRevision;
            this.BlockingRevision =  this.GetRevisionViewModel(this.version.Revisions.BlockingRevision);
            this.Revisions =  
                version
                    .Revisions
                    .Select(r => new RevisionViewModel(
                        r,
                        r.Number == SelectedRevision,
                        r.Number == BlockingRevision.Number))
                    .ToList();
        }

        private RevisionViewModel GetRevisionViewModel(IRevisionSetItem revision)
        {
            if (revision == null)
            {
                return new EmptyRevisionViewModel();
            }
            return new RevisionViewModel(revision);
        }

        public IEnumerable<IssueViewModel> AllIssues
        {
            get
            {
                return this.version.Revisions.AllIssues.Select(i => new IssueViewModel(i));
            }
        }

        public int BlockedRevisions
        {
            get
            {
                return this.version.Revisions.Count(r => r.IsBlocked);
            }
        }

        public RevisionViewModel BlockingRevision { get; set; }

        public string ComponentName { get; set; }

        public string EndRevisionNumber
        {
            get
            {
                if (this.version.EndRevision.HasValue)
                {
                    return this.version.EndRevision.Value.ToString(CultureInfo.CurrentCulture);
                }
                return "Head";
            }
        }

        public ReleaseComponentViewModel ReleaseComponent
        {
            get { return 
                new ReleaseComponentViewModel(
                    this.version.Component.Name,
                    this.version, null); }
        }

        public RevisionViewModel HighestReleasableRevision
        {
            get
            {
                return this.GetRevisionViewModel(this.version.Revisions.HighestReleasableRevision);
            }
        }

        public int IgnoreRevisions
        {
            get
            {
                return this.version.Revisions.Count(r => r.Ignore);
            }
        }

        public int NotReadyRevisions
        {
            get
            {
                return this.version.Revisions.Count(r => !r.CanBeReleased);
            }
        }

        public int ReadyRevisions
        {
            get
            {
                return this.version.Revisions.Count(r => r.CanBeReleased);
            }
        }

        public string ReleasableCommits
        {
            get
            {
                return this.version.Revisions.ReleasableCommits.ToString(CultureInfo.CurrentCulture);
            }
        }

        public string ReleaseName { get; set; }

        public long? SelectedRevision { get; set; }

        public IList<RevisionViewModel> Revisions { get; private set; }

        public string StartRevisionNumber
        {
            get
            {
                return this.version.StartRevision.ToString(CultureInfo.CurrentCulture);
            }
        }

        public string TotalCommits
        {
            get
            {
                return this.version.Revisions.Count().ToString(CultureInfo.CurrentCulture);
            }
        }

        public IEnumerable<RevisionViewModel> WipDescending
        {
            get
            {
                if (this.HighestReleasableRevision.IsEmpty)
                {
                    return this.version.Revisions.OrderByDescending(r=>r.Number).Select(r => new RevisionViewModel(r));
                }
                return this.version.Revisions
                    .OrderByDescending(r => r.Number)
                    .Where(r => (r.Number > this.version.Revisions.HighestReleasableRevision.Number))
                    .Select(r => new RevisionViewModel(r));
            }
        }
    }
}

