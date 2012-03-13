using WatiN.Core;

namespace ReleaseManager.FunctionalTests.Driver
{
    public class ComponentList : WatinPageDriver
    {
        public ComponentList(TestDriver driver)
            : base(driver)
        {
        }

        public virtual ReleaseList GoToReleaseList()
        {
            Browser.Link("goToReleaseList").Click();
            return Driver.ReleaseList();
        }
    }
}
