using WatiN.Core;

namespace ReleaseManager.FunctionalTests.Driver
{
    public class ReleaseList: WatinPageDriver
    {
        public ReleaseList(TestDriver driver)
            : base(driver)
        {
        }

        public virtual ComponentList GoToComponentList()
        {
            Browser.Link("goToComponentList").Click();
            return Driver.ComponentList();
        }
    }
}
