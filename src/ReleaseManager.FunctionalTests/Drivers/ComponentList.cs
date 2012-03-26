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
}
