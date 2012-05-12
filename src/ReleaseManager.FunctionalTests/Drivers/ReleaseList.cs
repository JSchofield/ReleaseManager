using WatiN.Core;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class ReleaseList : WatinPageDriver
    {
        public ReleaseList(TestDriver driver)
            : base(driver)
        {
        }

        public virtual AddRelease AddRelease()
        {
            Browser.Link("addRelease").Click();
            return CreatePageDriver<AddRelease>();
        }

        public virtual ReleaseSummary GoToRelease(string releaseName)
        {
            Browser.Link("goTo" + releaseName + "Summary").Click();
            return CreatePageDriver<ReleaseSummary>();
        }

        public virtual ReleaseList DeleteRelease(string releaseName)
        {
            Browser.Link("delete" + releaseName).Click();
            return this;
        }
    }
}
