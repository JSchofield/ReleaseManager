using System;
using System.Configuration;
using System.Globalization;

namespace ReleaseManager.Jira
{
    public class JiraConfig : ConfigurationSection, IJiraConfig
    {
        private const string sectionName = "jiraClient";

        [ConfigurationProperty("userName")]
        public string UserName
        {
            get { return GetBaseValue<string>("userName"); }
        }

        [ConfigurationProperty("password")]
        public string Password
        {
            get { return GetBaseValue<string>("password"); }
        }
        
        [ConfigurationProperty("cacheExpireTime")]
        public TimeSpan CacheExpireTime
        {
            get { return GetBaseValue<TimeSpan>("cacheExpireTime"); }
        }

        [ConfigurationProperty("baseUri")]
        public Uri BaseUri
        {
            get { return GetBaseValue<Uri>("baseUri"); }
        }

        [ConfigurationProperty("statusMap")]
        public StatusMap StatusMap
        {
            get { return GetBaseValue<StatusMap>("statusMap"); }
        }

        IStatusMap IJiraConfig.StatusMap
        {
            get { return this.StatusMap; }
        }

        private void ValidateProperties()
        {
            if (string.IsNullOrEmpty(Password))
            {
                throw new JiraConfigException("The jiraClient/@password configuration attribute is missing or does not have a value.");
            }
            if (string.IsNullOrEmpty(UserName))
            {
                throw new JiraConfigException("The jiraClient/@userName configuration attribute is missing or does not have a value.");
            }
            if (CacheExpireTime == default(TimeSpan))
            {
                throw new JiraConfigException("The jiraClient/@cacheExpireTime configuration attribute is missing or does not have a value or is zero.");
            }
            if (BaseUri == null)
            {
                throw new JiraConfigException("The jiraClient/@baseUri configuration attribute is missing or does not have a value.");
            }
            if (StatusMap.Count == 0)
            {
                throw new JiraConfigException("The jiraClient/statusMap configuration element is missing or does not contain any items.");
            }
        }

        private T GetBaseValue<T>(string key) 
        {
            object obj = base[key];
            if (obj == null)
            {
                string message = 
                    string.Format(CultureInfo.CurrentCulture,
                        "Could not find jiraClient configuration node '{0}'. The XML node may be missing or incorrectly named.",
                        key);

                throw new JiraConfigException(message);
            }
            try
            {
                var result = (T)obj;
                return result;
            }
            catch (InvalidCastException)
            {
                string message =
                    string.Format(CultureInfo.CurrentCulture,
                        "Could not load jiraClient configuration node '{0}'. The value '{1}' cannot be cast to type <{2}>.",
                        key, obj, typeof(T).Name);

                throw new JiraConfigException(message);
            }
        }

        static public IJiraConfig GetConfiguration()
        {
            return LoadConfiguration(ConfigurationManager.GetSection(sectionName));
        }

        static public IJiraConfig GetConfiguration(Configuration configuration)
        {
            return LoadConfiguration(configuration.GetSection(sectionName));
        }

        static private IJiraConfig LoadConfiguration(object section)
        {
            if (section == null)
            {
                throw new JiraConfigException("Could not find the jiraClient configuration section.");
            }
            var jiraConfig = section as JiraConfig;
            if (jiraConfig == null)
            {
                throw new JiraConfigException("Could not find the jiraClient configuration section.");
            }

            jiraConfig.ValidateProperties();
            return jiraConfig;
        }
    }
}
