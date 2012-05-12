using System;
using System.Text.RegularExpressions;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class EditRelease : WatinPageDriver
    {
        public EditRelease(TestDriver driver) : base(driver)
        {
        }

        public ReleaseSummary GoToSummary()
        {
            Browser.Link("goToSummary").Click();
            return CreatePageDriver<ReleaseSummary>();
        }

        public ReleaseWip GoToWIP()
        {
            Browser.Link("goToWIP").Click();
            return CreatePageDriver<ReleaseWip>();
        }

        public ReleaseAllTickets GoToAllTickets()
        {
            Browser.Link("goToAllTickets").Click();
            return CreatePageDriver<ReleaseAllTickets>();
        }

        public virtual EditRelease Save()
        {
            Browser.Button("save").Click();
            return CreatePageDriver<EditRelease>();
        }

        public virtual EditRelease SetName(string name)
        {
            Browser.TextField("Name").Value = name;
            return this;
        }

        public virtual EditRelease SetReleaseManager(string releaseManager)
        {
            Browser.TextField("ReleaseManager").Value = releaseManager;
            return this;
        }

        public virtual EditRelease SetReleaseDate(string releaseDate)
        {
            Browser.TextField("ReleaseDate").Value = releaseDate;
            return this;
        }

        public virtual new EditRelease SetValues(object keyValuePairs)
        {
            base.SetValues(keyValuePairs);
            return this;
        }

        public virtual EditReleaseComponent<EditRelease> Component(string name)
        {
            var index = GetComponentIndex(name);
            var comp = CreatePageDriver<EditReleaseComponent<EditRelease>>();
            comp.Index = index;
            comp.Parent = this;
            return comp;
        }

        private int GetComponentIndex(string name)
        {
            Regex re = new Regex(@"Components_(?<index>\d+)__Name");

            var field = Browser.TextField(f => (f.Id == null || f.Value == null) ? false : (re.IsMatch(f.Id) && f.Value == name));

            if (field != null)
            {
                var match = re.Match(field.Id);
                return Convert.ToInt32(match.Groups["index"].Value);
            }
            throw new ArgumentException("The component doesn't exist");

        }
    }
}
