namespace ReleaseManager.Web.Models
{
    using System.Collections.Generic;

    public class VersionUpdateModel
    {
        public IList<RevisionUpdateModel> Revisions { get; set; }
        public int Selected { get; set; }
        public string ReleaseName { get; set; }
        public string ComponentName { get; set; }
    }
}