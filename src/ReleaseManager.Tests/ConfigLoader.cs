namespace ReleaseManager.Tests
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using ReleaseManager.Jira;

    public static class ConfigLoader
    {
        static public Configuration Load(string configurationFile)
        {
            var fileMap =
                new ExeConfigurationFileMap {
                    ExeConfigFilename = configurationFile};
                        
            var config =
                ConfigurationManager.OpenMappedExeConfiguration(
                        fileMap,
                        ConfigurationUserLevel.None);

            if (config == null)
            {
                throw new NullReferenceException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "ConfigLoader failed to Load configuration file from {0}",
                        configurationFile));
            }
            return config;
        }
    }
}
