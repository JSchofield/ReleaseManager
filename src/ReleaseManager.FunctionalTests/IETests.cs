using System;
using NUnit.Framework;
using WatiN.Core;
using ReleaseManager.FunctionalTests.Driver;

namespace ReleaseManager.FunctionalTests
{
    [TestFixture]
    public class IETests
    {
        [Test]
        [RequiresSTA]
        public void NavigateAround()
        {
            using (var browser = new IE("http://localhost:57228/"))
            {
                var driver = new TestDriver(browser, true, TimeSpan.FromSeconds(1));

                var home = driver.HomePage();
                var releases = home.GoToReleaseList();
                var components = releases.GoToComponentList();
                components.GoToReleaseList();
            }
        }


    }
}
