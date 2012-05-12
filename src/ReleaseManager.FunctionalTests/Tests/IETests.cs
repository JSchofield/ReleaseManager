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
                    .Component("C1")
                        .Include()
                        .SetStartRevision("31")
                        .SetEndRevision("45")
                        .Parent
                    .Component("C2")
                        .Include()
                        .SetStartRevision("1234")
                        .SetEndRevision("HEAD")
                        .Parent
                    .Save();


        }
    }
}
