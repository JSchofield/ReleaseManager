using System;

namespace ReleaseManager.FunctionalTests.Driver
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

        public virtual new AddRelease SetValues(object keyValuePairs)
        {
            base.SetValues(keyValuePairs);
            return this;
        }
    }
}
