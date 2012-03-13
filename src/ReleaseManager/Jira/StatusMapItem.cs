using System.Configuration;

namespace ReleaseManager.Jira
{
    public class StatusMapItem: ConfigurationElement
    {
        [ConfigurationProperty("status")]
        public string Status
        {
            get { return base["status"].ToString(); }
        }

        [ConfigurationProperty("canBeReleased")]
        public bool CanBeReleased
        {
            get { return (bool)base["canBeReleased"]; }
        }
    }
}