using System;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class ReleaseAllTickets : WatinPageDriver
    {
        public ReleaseAllTickets(TestDriver driver) : base(driver)
        {
        }
        
        public ReleaseSummary GoToSummary()
        {
            Browser.Link("goToSummary").Click();
            return CreatePageDriver<ReleaseSummary>();
        }

        public ReleaseWip GoToWIP()
        {
            Browser.Link("goToWIP").Click();
            return CreatePageDriver<ReleaseWip>();
        }

        public EditRelease GoToEdit()
        {
            Browser.Link("goToEdit").Click();
            return CreatePageDriver<EditRelease>();
        }
    }
}
