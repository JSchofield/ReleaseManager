namespace ReleaseManager.FunctionalTests.Drivers
{

    public class ReleaseSummary : WatinPageDriver
    {
        public ReleaseSummary(TestDriver driver) : base(driver)
        {
        }

        public virtual EditRelease GoToEdit()
        {
            Browser.Link("goToEdit").Click();
            return CreatePageDriver<EditRelease>();
        }

        public virtual ReleaseWip GoToWIP()
        {
            Browser.Link("goToWIP").Click();
            return CreatePageDriver<ReleaseWip>();
        }

        public virtual ReleaseAllTickets GoToAllTickets()
        {
            Browser.Link("goToAllTickets").Click();
            return CreatePageDriver<ReleaseAllTickets>();
        }

        public virtual VersionCommits GoToVersion(string component)
        {
            Browser.Link(l => l.Text == component);
            return CreatePageDriver<VersionCommits>();
        }
    }
}
