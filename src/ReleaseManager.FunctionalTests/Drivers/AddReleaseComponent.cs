using System;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class AddReleaseComponent : WatinPageDriver
    {
        private readonly int _index;
        private readonly AddRelease _parent;

        public AddReleaseComponent(AddRelease parent, int index)
            : base(parent.Driver)
        {
            _index = index;
            _parent = parent;
        }

        public AddReleaseComponent Include()
        {
            Browser.CheckBox(string.Format("Components_{0}__Included", _index)).Checked = true;
            return this;
        }

        public AddReleaseComponent Exclude()
        {
            Browser.CheckBox(string.Format("Components_{0}__Included", _index)).Checked = false;
            return this;
        }

        public AddRelease Parent
        {
            get
            {
                return _parent;
            }
        }

    }
}
