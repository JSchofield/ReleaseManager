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

        public virtual ComponentList Activate(string component)
        {
            return this;
        }

        public virtual ComponentList Deactivate(string component)
        {
            return this;
        }

        public virtual ComponentList SetLocation(string location)
        {
            return this;
        }
    }
}
