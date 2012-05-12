using System;
using NUnit.Framework;

namespace ReleaseManager.FunctionalTests.Tests
{
    [TestFixture]
    public class IETests : DevServerTestBase
    {
        public IETests() : base(true, TimeSpan.FromMilliseconds(500))
        {
        }

        [Test, RequiresSTA]
        public void NavigateAround()
        {
            var home = Driver.HomePage();
            home.GoToReleaseList()
                .AddRelease()
                    .SetValues(new { Name = "My Release", ReleaseManager = "Jonathon", ReleaseDate = "2012-03-31" })
                    .Save()
                .GoToReleaseList()
                .AddRelease()
                    .SetValues(new { Name = "My Second Release", ReleaseManager = "Jonathon", ReleaseDate = "2012-04-30" })
                    .Save()
                .GoToReleaseList()
                    .GoToRelease("My Release")
                .GoToReleaseList()
                    .DeleteRelease("My Release");
        }

        [Test, RequiresSTA]
        public void CreateRelease()
        {
            var home = Driver.HomePage();
            home.GoToComponentList()
                .AddComponent()
                    .SetValues(new {Name = "Comp1", Location = "svn-somewhere" })
                    .Save()
                .GoToReleaseList()
                .AddRelease()
                    .SetValues(new { Name = "My Release", ReleaseManager = "Jonathon", ReleaseDate = "2012-03-31" })
                    .Component("Comp1")
                        .Include().Parent
                    .Save()
                .GoToReleaseList();
        }
    }
}
