using System;
using System.Runtime.Serialization;

namespace ReleaseManager.Subversion
{
    [Serializable]
    public class SvnGetLogException : Exception
    {
        public SvnGetLogException()
        {}

        public SvnGetLogException(string message) : base(message)
        {}

        public SvnGetLogException(string message, Exception innerException) : base(message, innerException)
        {}

        protected SvnGetLogException(SerializationInfo info, StreamingContext context): base(info, context)
        {}
    }
}

