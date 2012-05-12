using System;
using NUnit.Framework;

namespace ReleaseManager.FunctionalTests.Tests
{
    //TODO: Provide options for running tests with different databases, option to not tear down the web server between tests, which would require the ability to reset the database
    // between tests.

    [TestFixture]
    public class IETests : DevServerTestBase
    {
        public IETests() : base(true, TimeSpan.FromMilliseconds(200))
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

        [Test, RequiresSTA]
        public void UpdateComponent()
        {
            var home = Driver.HomePage();
            home.GoToComponentList()
                .AddComponent()
                    .SetValues(new { Name = "Comp1", Location = "svn-somewhere" })
                    .Save()
                    .SetLocation("Comp1", "svn:\\another.repo")
                    .Save();
        }

        [Test, RequiresSTA]
        public void SetRevisionsOfReleaseComponents()
        {
            var home = Driver.HomePage();
            home.GoToComponentList()
                .AddComponent()
                    .SetValues(new { Name = "C1", Location = @"svn:\\repo\c1" })
                    .Save()
                .AddComponent()
                    .SetValues(new { Name = "C2", Location = @"svn:\\repo\c2" })
                    .Save()
                .AddComponent()
                    .SetValues(new { Name = "C3", Location = @"svn:\\repo\c3" })
                    .Save()
                .GoToReleaseList()                    
                .AddRelease()
                    .SetValues(new { Name = "My Release", ReleaseManager = "Jonathon", ReleaseDate = "2012-03-31" })
                    .Component("C2")
                        .Include()
                        .SetValues(new { StartRevision = "1234", EndRevision = "HEAD" })
                        .Parent
                    .Component("C1")
                        .Include()
                        .SetValues(new { StartRevision = "453", EndRevision = "480" })
                        .Parent
                    .Save();
        }

        [Test, RequiresSTA]
        public void EditReleaseAndComponents()
        {
            var home = Driver.HomePage();
            home.GoToComponentList()
                .AddComponent()
                    .SetValues(new { Name = "C1", Location = @"svn:\\repo\c1" })
                    .Save()
                .AddComponent()
                    .SetValues(new { Name = "C2", Location = @"svn:\\repo\c2" })
                    .Save()
                .GoToReleaseList()
                .AddRelease()
                    .SetValues(new { Name = "My Release", ReleaseManager = "Jonathon", ReleaseDate = "2012-03-31" })
                    .Component("C1")
                        .Include()
                        .Parent
                    .Save()
                .SetValues(new { ReleaseDate = "01/01/2013" })
                .Component("C1")
                    .Exclude()
                    .Parent
                .Component("C2")
                    .Include()
                    .SetValues(new { StartRevision = "3", EndRevision = "4" })
                    .Parent
                .Save();
        }
    }
}
