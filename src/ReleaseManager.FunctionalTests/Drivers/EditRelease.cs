﻿using System;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class EditRelease : WatinPageDriver
    {
        public EditRelease(TestDriver driver) : base(driver)
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

        public ReleaseAllTickets GoToAllTickets()
        {
            Browser.Link("goToAllTickets").Click();
            return CreatePageDriver<ReleaseAllTickets>();
        }
    }
}
