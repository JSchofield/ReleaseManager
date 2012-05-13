using System;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class ReleaseWip : WatinPageDriver
    {
        public ReleaseWip(TestDriver driver) : base(driver)
        {
        }


        public virtual EditRelease GoToEdit()
        {
            Browser.Link("goToEdit").Click();
            return CreatePageDriver<EditRelease>();
        }

        public virtual ReleaseSummary GoToSummary()
        {
            Browser.Link("goToSummary").Click();
            return CreatePageDriver<ReleaseSummary>();
        }

        public virtual ReleaseAllTickets GoToAllTickets()
        {
            Browser.Link("goToAllTickets").Click();
            return CreatePageDriver<ReleaseAllTickets>();
        }
    }
}
