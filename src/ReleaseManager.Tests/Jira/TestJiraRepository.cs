using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ReleaseManager.Jira;

namespace ReleaseManager.Tests.Jira
{
    public static class InMemoryIssueRepository
    {
        private class StatusMap : IStatusMap
        {
            Dictionary<string, bool> statuses;

            public StatusMap()
            {
                statuses = new Dictionary<string, bool>();
                statuses["Open"] = false;
                statuses["Closed"] = true;
            }

            public bool this[string status]
            {
                get { return statuses[status]; }
            }
        }

        public static IIssueRepository CreateIssueRepository()
        {
            return new JiraRepository(new StubJiraClient(), new Uri("http://jira"), TimeSpan.FromMinutes(2), new StatusMap());
        }
    }



    [TestFixture]
	public class TestJiraRepository
	{


        [Test]
        public void Test()
        {
            var stub = new StubJiraClient();
            var repo = new JiraRepository(stub, new Uri("http://jira"), TimeSpan.FromMinutes(2), new StatusMap());

            stub.AddIssue("TEST-1", "tester", "Open", "Do some work");

            IIssue issue = repo.GetIssue("TEST-1");

            Assert.AreEqual("Open", issue.Status);


        }
	}
}
