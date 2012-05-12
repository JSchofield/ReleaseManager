using System;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class AddReleaseComponent : WatinPageDriver
    {
        public virtual int Index { get; set; }
        public virtual AddRelease Parent { get; set; }

        public AddReleaseComponent(TestDriver driver) : base(driver)
        {
        }

        public AddReleaseComponent(AddRelease parent, int index)
            : base(parent.Driver)
        {
            Index = index;
            Parent = parent;
        }

        public virtual AddReleaseComponent Include()
        {
            Browser.CheckBox(string.Format("Components_{0}__Included", Index)).Checked = true;
            return this;
        }

        public virtual AddReleaseComponent Exclude()
        {
            Browser.CheckBox(string.Format("Components_{0}__Included", Index)).Checked = false;
            return this;
        }

        public virtual AddReleaseComponent SetStartRevision(string revision)
        {
            Browser.TextField(string.Format("Components_{0}__StartRevision", Index)).Value = revision;
            return this;
        }

        public virtual AddReleaseComponent SetEndRevision(string revision)
        {
            Browser.TextField(string.Format("Components_{0}__EndRevision", Index)).Value = revision;
            return this;
        }
    }
}
