// ReSharper disable InconsistentNaming
namespace ReleaseManager.Tests.Subversion
{
    using System.Configuration;
    using NUnit.Framework;
    using ReleaseManager.Subversion;

    public class SvnConfigSpecs : BehaviorDrivenSpecificationBase
    {
        protected SvnConfig svnConfig;

        protected override void EstablishContext()
        {}

        protected override void When()
        {}

        protected void When_config_file_is(string configFile)
        {
            string path = string.Concat(@"Subversion\TestConfigurations\", configFile);
            Configuration appConfig = ConfigLoader.Load(path);
            svnConfig = (SvnConfig)SvnConfig.GetConfiguration(appConfig);
        }
    }

    [TestFixture]
    [Category("SvnConfig")]
    public class When_subversionClient_config_section_is_valid: SvnConfigSpecs
    {
        protected override void When()
        {
            When_config_file_is("Valid.xml");
        }

        [Test]
        public void UserName_gets_configured_value()
        {
            Assert.AreEqual("userName", svnConfig.UserName);
        }

        [Test]
        public void Password_gets_configured_value()
        {
            Assert.AreEqual("password", svnConfig.Password);
        }
    }


    [TestFixture]
    [Category("SvnConfig")]
    public class When_subversionClient_config_section_is_missing : SvnConfigSpecs
    {
        protected override void When()
        {
            When_config_file_is("MissingSection.xml");
        }

        [Test]
        public void SvnConfig_throws_exception_indicating_the_section_is_missing()
        {
            Assert.IsInstanceOf(typeof(SvnConfigException), ExceptionThrown);
            Assert.That(ExceptionThrown.Message.Contains("Could not find the subversionClient configuration section"));
        }

    }

    [TestFixture]
    [Category("SvnConfig")]
    public class When_subversionClient_config_element_has_no_password : SvnConfigSpecs
    {
        protected override void When()
        {
            When_config_file_is("NoPassword.xml");
        }

        [Test]
        public void SvnConfig_throws_exception_indicating_the_password_is_missing()
        {
            Assert.IsInstanceOf(typeof(SvnConfigException), ExceptionThrown);
            Assert.That(ExceptionThrown.Message.Contains("password"));
        }
    }

    [TestFixture]
    [Category("SvnConfig")]
    public class When_subversionClient_config_element_has_no_userName : SvnConfigSpecs
    {
        protected override void When()
        {
            When_config_file_is("NoUserName.xml");
        }

        [Test]
        public void SvnConfig_throws_exception_indicating_the_userName_is_missing()
        {
            Assert.IsInstanceOf(typeof(SvnConfigException), ExceptionThrown);
            Assert.That(ExceptionThrown.Message.Contains("userName"));
        }
    }

}
// ReSharper restore InconsistentNaming