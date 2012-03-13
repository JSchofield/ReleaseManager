// ReSharper disable InconsistentNaming
namespace ReleaseManager.Tests.Jira
{
    using System;
    using System.Configuration;
    using NUnit.Framework;
    using ReleaseManager.Jira;

    public class JiraConfigSpecs : BehaviorDrivenSpecificationBase
    {
        protected JiraConfig jiraConfig;

        protected override void EstablishContext()
        {}

        protected override void When()
        {}

        protected void When_config_file_is(string configFile)
        {
            string path = string.Concat(@"Jira\TestConfigurations\", configFile);
            Configuration appConfig = ConfigLoader.Load(path);
            jiraConfig = (JiraConfig)JiraConfig.GetConfiguration(appConfig);
        }
    }

    [TestFixture]
    [Category("JiraConfig")]
    public class When_jiraClient_config_section_is_valid : JiraConfigSpecs
    {
        protected override void When()
        {
            When_config_file_is("Valid.xml");
        }

        [Test]
        public void UserName_gets_configured_value()
        {
            Assert.AreEqual("userName", jiraConfig.UserName);
        }

        [Test]
        public void Password_gets_configured_value()
        {
            Assert.AreEqual("superSecurePassword", jiraConfig.Password);
        }

        [Test]
        public void BaseUri_gets_configured_value_as_absolute_Uri()
        {
            Assert.IsInstanceOf(typeof(Uri), jiraConfig.BaseUri);
            Assert.AreEqual(new Uri("http://this.is.where.jira.lives"), jiraConfig.BaseUri.ToString());
        }

        [Test]
        public void CacheExpireTime_gets_configured_value_as_TimeSpan()
        {
            Assert.IsInstanceOf(typeof(TimeSpan), jiraConfig.CacheExpireTime);
            Assert.AreEqual(TimeSpan.FromMinutes(1), jiraConfig.CacheExpireTime);
        }

        [Test]
        public void StatusMap_provides_CanBeReleased_for_Jira_states()
        {
            Assert.AreEqual(false, (jiraConfig.StatusMap as IStatusMap)["Open"]);
            Assert.AreEqual(true, (jiraConfig.StatusMap as IStatusMap)["Ready for release"]);
        }
    }

    [TestFixture]
    [Category("JiraConfig")]
    public class When_jiraClient_config_section_is_missing : JiraConfigSpecs
    {
        protected override void When()
        {
            When_config_file_is("MissingSection.xml");
        }

        [Test]
        public void JiraConfig_throws_exception_indicating_the_section_is_missing()
        {
            Assert.IsInstanceOf(typeof(JiraConfigException), ExceptionThrown);
            Assert.That(ExceptionThrown.Message.Contains("Could not find the jiraClient configuration section"));
        }

    }

    [TestFixture]
    [Category("JiraConfig")]
    public class When_jiraClient_config_element_has_no_password : JiraConfigSpecs
    {
        protected override void When()
        {
            When_config_file_is("NoPassword.xml");
        }

        [Test]
        public void JiraConfig_throws_exception_indicating_the_password_is_missing()
        {
            Assert.IsInstanceOf(typeof(JiraConfigException), ExceptionThrown);
            Assert.That(ExceptionThrown.Message.Contains("password"));
        }
    }

    [TestFixture]
    [Category("JiraConfig")]
    public class When_jiraClient_config_element_has_no_userName : JiraConfigSpecs
    {
        protected override void When()
        {
            When_config_file_is("NoUserName.xml");
        }

        [Test]
        public void JiraConfig_throws_exception_indicating_the_userName_is_missing()
        {
            Assert.IsInstanceOf(typeof(JiraConfigException), ExceptionThrown);
            Assert.That(ExceptionThrown.Message.Contains("userName"));
        }
    }

    [TestFixture]
    [Category("JiraConfig")]
    public class When_jiraClient_config_element_has_no_cacheExpireTime : JiraConfigSpecs
    {
        protected override void When()
        {
            When_config_file_is("NoCacheExpireTime.xml");
        }

        [Test]
        public void JiraConfig_throws_exception_indicating_the_cacheExpireTime_is_missing()
        {
            Assert.IsInstanceOf(typeof(JiraConfigException), ExceptionThrown);
            Assert.That(ExceptionThrown.Message.Contains("cacheExpireTime"));
        }
    }

    [TestFixture]
    [Category("JiraConfig")]
    public class When_jiraClient_config_element_has_no_baseUri : JiraConfigSpecs
    {
        protected override void When()
        {
            When_config_file_is("NoBaseUri.xml");
        }

        [Test]
        public void JiraConfig_throws_exception_indicating_the_baseUri_is_missing()
        {
            Assert.IsInstanceOf(typeof(JiraConfigException), ExceptionThrown);
            Assert.That(ExceptionThrown.Message.Contains("baseUri"));
        }
    }

    [TestFixture]
    [Category("JiraConfig")]
    public class When_jiraClient_config_element_has_no_statusMap : JiraConfigSpecs
    {
        protected override void When()
        {
            When_config_file_is("NoStatusMap.xml");
        }

        [Test]
        public void JiraConfig_throws_exception_indicating_the_statusMap_is_missing()
        {
            Assert.IsInstanceOf(typeof(JiraConfigException), ExceptionThrown);
            Assert.That(ExceptionThrown.Message.Contains("statusMap"));
        }
    }

    [TestFixture]
    [Category("JiraConfig")]
    public class When_jiraClient_config_element_has_no_statusMapItems : JiraConfigSpecs
    {
        protected override void When()
        {
            When_config_file_is("NoStatusMapItems.xml");
        }

        [Test]
        public void JiraConfig_throws_exception_indicating_the_statusMapItems_are_missing()
        {
            Assert.IsInstanceOf(typeof(JiraConfigException), ExceptionThrown);
            Assert.That(ExceptionThrown.Message.Contains("statusMap"));
        }
    }
}
// ReSharper restore InconsistentNaming