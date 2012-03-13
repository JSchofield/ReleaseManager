namespace ReleaseManager.Web.Models
{
    using System.Globalization;
    using System.Web.Mvc;

    public class ReleaseComponentViewModel
    {
        public ReleaseComponentViewModel()
        {
            
        }

        public ReleaseComponentViewModel(string name, IVersionWork version, long? sugestedNextStartRevision)
        {
            this.Name = name;
            if (version != null)
            {
                this.Included = true;
                this.StartRevision = version.StartRevision.ToString(CultureInfo.CurrentCulture);
                this.EndRevision = version.EndRevision.HasValue ? version.EndRevision.Value.ToString(CultureInfo.CurrentCulture) : "Head";
                this.SelectedRevision = version.SelectedRevision.ToString();
            }
            else
            {
                this.Included = false;
                this.StartRevision = (sugestedNextStartRevision ?? 0).ToString(CultureInfo.CurrentCulture);
                this.EndRevision = "Head";
                this.SelectedRevision = string.Empty;
            }
        }

        [HiddenInput]
        public string Name { get; set; }

        public bool Included { get; set; }

        public string StartRevision { get; set; }

        public string EndRevision { get; set; }

        public string SelectedRevision { get; set; }
    }
}

