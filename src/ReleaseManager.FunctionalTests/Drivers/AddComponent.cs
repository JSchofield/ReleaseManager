using System;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class AddComponent : WatinPageDriver
    {
        public AddComponent(TestDriver driver)
            : base(driver)
        {
        }

        public virtual new AddComponent SetValues(object keyValuePairs)
        {
            base.SetValues(keyValuePairs);
            return this;
        }

        public virtual ComponentList Save()
        {
            Browser.Button("save").Click();
            return CreatePageDriver<ComponentList>();
        }
    }
}
