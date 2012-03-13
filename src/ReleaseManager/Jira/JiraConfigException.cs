using System;
using System.Runtime.Serialization;

namespace ReleaseManager.Jira
{
    [Serializable]
    public class JiraConfigException : Exception
    {
        public JiraConfigException()
        { }

        public JiraConfigException(string message)
            : base(message)
        { }

        public JiraConfigException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected JiraConfigException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}