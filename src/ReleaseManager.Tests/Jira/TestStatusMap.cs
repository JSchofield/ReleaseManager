// ReSharper disable InconsistentNaming
namespace ReleaseManager.Tests.Jira
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.Globalization;
    using NUnit.Framework;
    using ReleaseManager.Jira;

    [TestFixture]
    [Category("StatusMap")]
    public class When_the_statusMap_has_ten_valid_items : JiraConfigSpecs
    {
        private IStatusMap statusMap;

        protected override void When()
        {
            When_config_file_is("Valid.xml");
            statusMap = jiraConfig.StatusMap;
        }

        [Test]
        public void Can_get_CanBeReleased_value_for_statuses()
        {
            Assert.False(statusMap["Open"]);
            Assert.True(statusMap["Ready for release"]);
        }

        [Test]
        public void Unknown_status_returns_false()
        {
            Assert.False(statusMap["This status doesn't exist"]);
        }

        // This test is here to convince me that the ConfigurationElementCollection
        // scaffolding doesn't add significant overhead to status lookups
        [Test, Ignore("Performance test")]
        public void Show_duration_for_1000_status_lookups()
        {
            var testStatuses = new [] {
                "Open",
                "Ready for release",
                "Sprint candidate",
                "In QA",
                "Dev done",
                "Dev. Done" ,
                "Deployed",
                "Closed",
                "In Progress",
                "Resolved"};

            bool result = false; // result exists only so that statusMap[string] can be invoked
            const int numberOfCalls = 1000;
            var rand = new Random();

            var startTime = DateTime.Now;
            for (var counter = 0; counter < numberOfCalls; counter +=1)
            {
                result = statusMap[testStatuses[rand.Next(0, 9)]];
            }
            var duration = DateTime.Now - startTime;

            Assert.Less(duration, TimeSpan.FromMilliseconds(100));
            Assert.NotNull(result); // this prevents resharper warnings

            var message = string.Format(CultureInfo.CurrentCulture,
                "{0} calls to StatusMap[string] took a total of {1}ms.",
                numberOfCalls,
                duration.TotalMilliseconds);

            Debug.WriteLine(message);
            Console.WriteLine(message);
        }
    }
}
// ReSharper restore InconsistentNaming