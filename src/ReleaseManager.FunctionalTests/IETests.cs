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
            using (var driver = new TestDriver(new IE("http://localhost:57228/"), true, TimeSpan.FromSeconds(0)))
            {
                var home = driver.HomePage();
                home.GoToReleaseList()
                    .AddRelease()
                        .SetValues(new { Name = "My Release", ReleaseManager = "Jonathon", ReleaseDate = "2012-03-31" })
                        .Save()
                    .GoToReleaseList()
                    .AddRelease()
                        .SetValues(new { Name = "My Second Release", ReleaseManager = "Jonathon", ReleaseDate = "2012-04-30" })
                        .Save()
                    .GoToReleaseList();
            }
        }
    }
}
