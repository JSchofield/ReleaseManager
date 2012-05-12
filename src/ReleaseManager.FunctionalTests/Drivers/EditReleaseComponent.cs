using System;
using System.Linq;
using System.Collections.Generic;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class EditReleaseComponent<T> : WatinPageDriver where T : WatinPageDriver
    {
        public int Index { get; set; }
        public T Parent { get; set; }

        public EditReleaseComponent(TestDriver driver) : base(driver)
        {
        }

        public EditReleaseComponent(T parent, int index) 
            : base(parent.Driver)
        {
            Index = index;
            Parent = parent;
        }

        public virtual EditReleaseComponent<T> Include()
        {
            Browser.CheckBox(string.Format("Components_{0}__Included", Index)).Checked = true;
            return this;
        }

        public virtual EditReleaseComponent<T> Exclude()
        {
            Browser.CheckBox(string.Format("Components_{0}__Included", Index)).Checked = false;
            return this;
        }

        public virtual new EditReleaseComponent<T> SetValues(object keyValuePairs)
        {
            var newKeyValuePairs = 
                AnonymousObjectToDictionary(keyValuePairs)
                .Select(pair => new KeyValuePair<string,string>(
                    string.Format("Components_{0}__{1}", Index, pair.Key), 
                    pair.Value));

            base.SetValues(newKeyValuePairs);
            return this;
        }

        public virtual EditReleaseComponent<T> SetStartRevision(string revision)
        {
            Browser.TextField(string.Format("Components_{0}__StartRevision", Index)).Value = revision;
            return this;
        }

        public virtual EditReleaseComponent<T> SetEndRevision(string revision)
        {
            Browser.TextField(string.Format("Components_{0}__EndRevision", Index)).Value = revision;
            return this;
        }
    }
}
