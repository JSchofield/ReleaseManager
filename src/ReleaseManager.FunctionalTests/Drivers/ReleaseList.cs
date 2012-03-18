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
    }
}
