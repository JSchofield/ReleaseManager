using System;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class ReleaseSummary : WatinPageDriver
    {
        public ReleaseSummary(TestDriver driver) : base(driver)
        {
        }

        public EditRelease GoToEdit()
        {
            Browser.Link("goToEdit").Click();
            return CreatePageDriver<EditRelease>();
        }

        public ReleaseWip GoToWIP()
        {
            Browser.Link("goToWIP").Click();
            return CreatePageDriver<ReleaseWip>();
        }

        public ReleaseAllTickets GoToAllTickets()
        {
            Browser.Link("goToAllTickets").Click();
            return CreatePageDriver<ReleaseAllTickets>();
        }
    }
}
