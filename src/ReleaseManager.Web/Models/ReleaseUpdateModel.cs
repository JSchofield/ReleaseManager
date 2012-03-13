namespace ReleaseManager.Web.Models
{
    using System.Collections.Generic;

    public class ReleaseUpdateModel
    {
        public ReleaseUpdateModel()
        {
            this.Components = new List<ReleaseComponentViewModel>();
        }

        public ReleaseUpdateModel(IList<ReleaseComponentViewModel> components)
        {
            this.Components = components;
        }

        public ReleaseUpdateModel(IRelease release, IList<ReleaseComponentViewModel> components)
        {
            this.Components = components;
            this.Name = release.Name;
            this.ReleaseDate = release.ReleaseDate.HasValue ? release.ReleaseDate.Value.ToShortDateString() : string.Empty;
            this.ReleaseManager = release.ReleaseManager;
        }

        public IList<ReleaseComponentViewModel> Components { get; private set; }

        public string Name { get; set; }
        public string ReleaseDate { get; set; }
        public string ReleaseManager { get; set; }
    }
}