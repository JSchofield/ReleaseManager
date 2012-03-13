using System;
using System.Configuration;
using System.Globalization;
using SysConfig = System.Configuration;

namespace ReleaseManager.Subversion
{
    public class SvnConfig : SysConfig.ConfigurationSection, ISvnConfig
    {
        [SysConfig.ConfigurationProperty("userName")]
        public string UserName
        {
            get { return GetBaseValue<string>("userName"); }
        }

        [SysConfig.ConfigurationProperty("password")]
        public string Password
        {
            get { return GetBaseValue<string>("password"); }
        }

        private void ValidateProperties()
        {
            if (string.IsNullOrEmpty(Password))
            {
                throw new SvnConfigException("The subversionClient/@password configuration attribute is missing or does not have a value.");
            }
            if (string.IsNullOrEmpty(UserName))
            {
                throw new SvnConfigException("The subversionClient/@userName configuration attribute is missing or does not have a value.");
            }
        }

        private T GetBaseValue<T>(string key)
        {
            object obj = base[key];
            if (obj == null)
            {
                string message =
                    string.Format(CultureInfo.CurrentCulture,
                        "Could not find subversionClient configuration node '{0}'. The XML node may be missing or incorrectly named.",
                        key);

                throw new SvnConfigException(message);
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
                        "Could not load subversionClient configuration node '{0}'. The value '{1}' cannot be cast to type <{2}>.",
                        key, obj, typeof(T).Name);

                throw new SvnConfigException(message);
            }
        }

        static private ISvnConfig LoadConfiguration(object section)
        {
            if (section == null)
            {
                throw new SvnConfigException("Could not find the subversionClient configuration section.");
            }
            var svnConfig = section as SvnConfig;
            if (svnConfig == null)
            {
                throw new SvnConfigException("Could not find the subversionClient configuration section.");
            }
            svnConfig.ValidateProperties();
            return svnConfig;
        }

        static public ISvnConfig GetConfiguration()
        {
            return LoadConfiguration(ConfigurationManager.GetSection("subversionClient"));
        }

        static public ISvnConfig GetConfiguration(SysConfig.Configuration configuration)
        {
            return LoadConfiguration(configuration.GetSection("subversionClient"));
        }
    }
}
