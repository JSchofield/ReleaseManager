using System;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class AddReleaseComponent : WatinPageDriver
    {
        public int Index { get; set; }
        public AddRelease Parent { get; set; }

        public AddReleaseComponent(AddRelease parent, int index)
            : base(parent.Driver)
        {
            Index = index;
            Parent = parent;
        }

        public AddReleaseComponent Include()
        {
            Browser.CheckBox(string.Format("Components_{0}__Included", Index)).Checked = true;
            return this;
        }

        public AddReleaseComponent Exclude()
        {
            Browser.CheckBox(string.Format("Components_{0}__Included", Index)).Checked = false;
            return this;
        }

        public AddReleaseComponent SetStartRevision(string revision)
        {
            Browser.TextField(string.Format("Components_{0}__StartRevision", Index)).Value = revision;
            return this;
        }

        public AddReleaseComponent SetEndRevision(string revision)
        {
            Browser.TextField(string.Format("Components_{0}__EndRevision", Index)).Value = revision;
            return this;
        }
    }
}
