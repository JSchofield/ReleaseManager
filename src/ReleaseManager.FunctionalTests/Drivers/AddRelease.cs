using System;
using System.Text.RegularExpressions;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class AddRelease : WatinPageDriver
    {
        public AddRelease(TestDriver driver) : base(driver)
        {
        }

        public virtual EditRelease Save()
        {
            Browser.Button("save").Click();
            return CreatePageDriver<EditRelease>();
        }

        public virtual AddRelease SetName(string name)
        {
            Browser.TextField("Name").Value = name;
            return this;
        }

        public virtual AddRelease SetReleaseManager(string releaseManager)
        {
            Browser.TextField("ReleaseManager").Value = releaseManager;
            return this;
        }

        public virtual AddRelease SetReleaseDate(string releaseDate)
        {
            Browser.TextField("ReleaseDate").Value = releaseDate;
            return this;
        }

        public virtual new AddRelease SetValues(object keyValuePairs)
        {
            base.SetValues(keyValuePairs);
            return this;
        }

        public virtual AddReleaseComponent Component(string name)
        {
            var index = GetComponentIndex(name);
            return new AddReleaseComponent(this, index); 
        }

        private int GetComponentIndex(string name)
        {
            Regex re = new Regex(@"Components_(?<index>\d+)__Name");

            var field = Browser.TextField(f => re.IsMatch(f.Id) && f.Value == name);

            if (field != null)
            {
                var match = re.Match(field.Id);
                return Convert.ToInt32(match.Groups["index"].Value);
            }
            throw new ArgumentException("The component doesn't exist");
        
        }
    }
}
