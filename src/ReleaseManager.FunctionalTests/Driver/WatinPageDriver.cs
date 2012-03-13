using WatiN.Core;

namespace ReleaseManager.FunctionalTests.Driver
{
    public abstract class WatinPageDriver
    {
        public WatinPageDriver(TestDriver driver)
        {
            this.Driver = driver;
        }

        public Browser Browser { get { return Driver.Browser; } }

        public TestDriver Driver { get; private set; }
    }
}
