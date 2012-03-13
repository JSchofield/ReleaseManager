namespace ReleaseManager.Web.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class ReleaseViewModel
    {
        private readonly IRelease release;

        public ReleaseViewModel(IRelease release)
        {
            this.release = release;
        }

        public ReleaseViewModel(IRelease release, IEnumerable<IVersionWork> versions)
        {
            this.release = release;
            this.Versions = versions.Select(v => new VersionViewModel(v));
        }

        public string Name { get { return this.release.Name; } }

        public string ReleaseDate
        {
            get
            {
                if (this.release.ReleaseDate.HasValue)
                {
                    return this.release.ReleaseDate.Value.ToShortDateString();
                }
                return "TBA";
            }
        }

        public string ReleaseManager
        {
            get
            {
                if (string.IsNullOrEmpty(this.release.ReleaseManager))
                {
                    return "TBA";
                }
                return this.release.ReleaseManager;
            }
        }

        public IEnumerable<VersionViewModel> Versions { get; private set; }
    }
}