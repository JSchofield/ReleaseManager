using WatiN.Core;

namespace ReleaseManager.FunctionalTests.Driver
{
    public class Home : WatinPageDriver
    {
        public Home(TestDriver driver) : base(driver)
        {            
        }

        public virtual ComponentList GoToComponentList()
        {
            Browser.Link("goToComponentList").Click();
            return Driver.ComponentList();
        }

        public virtual ReleaseList GoToReleaseList()
        {
            Browser.Link("goToReleaseList").Click();
            return Driver.ReleaseList();
        }
    }
}
