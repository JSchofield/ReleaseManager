using System;
using System.Linq;
using System.Collections.Generic;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class AddReleaseComponent : WatinPageDriver
    {
        public int Index { get; set; }
        public AddRelease Parent { get; set; }

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

        public virtual new AddReleaseComponent SetValues(object keyValuePairs)
        {
            var newKeyValuePairs = 
                AnonymousObjectToDictionary(keyValuePairs)
                .Select(pair => new KeyValuePair<string,string>(
                    string.Format("Components_{0}__{1}", Index, pair.Key), 
                    pair.Value));

            base.SetValues(newKeyValuePairs);
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
