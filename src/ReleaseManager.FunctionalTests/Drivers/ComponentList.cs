using WatiN.Core;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class ComponentList : WatinPageDriver
    {
        public ComponentList(TestDriver driver) : base(driver)
        {
        }

        public virtual AddComponent AddComponent()
        {
            Browser.Link("addComponent").Click();
            return CreatePageDriver<AddComponent>();
        }
    }

    public class AddComponent : WatinPageDriver
    {
        public AddComponent(TestDriver driver) : base(driver)
        {
        }

        public virtual new AddComponent SetValues(object keyValuePairs)
        {
            base.SetValues(keyValuePairs);
            return this;
        }

        public virtual EditRelease Save()
        {
            Browser.Button("save").Click();
            return CreatePageDriver<EditRelease>();
        }
    }
}
