using System;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class VersionCommits : WatinPageDriver
    {
        public VersionCommits(TestDriver driver) : base(driver)
        {
        }

        public virtual ReleaseSummary GoToSummary()
        {
            Browser.Link("goToSummary").Click();
            return CreatePageDriver<ReleaseSummary>();
        }

       
    }
}
