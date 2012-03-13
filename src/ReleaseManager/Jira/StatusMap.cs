using System.Configuration;

namespace ReleaseManager.Jira
{
    public class StatusMap: ConfigurationElementCollection, IStatusMap
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new StatusMapItem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((StatusMapItem)element).Status;
        }
        
        bool IStatusMap.this[string status]
        {
            get
            {
                var item = (StatusMapItem) BaseGet(status);
                if (item == null)
                {
                    return false;
                }
                return item.CanBeReleased;
            }
        }
    }
}