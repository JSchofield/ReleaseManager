using SharpSvn;
using System;

namespace ReleaseManager.Subversion
{
    public class SvnLogEventArgsWrapper
    {
        private readonly SvnLogEventArgs args;

        public SvnLogEventArgsWrapper()
        { }

        public SvnLogEventArgsWrapper(SvnLogEventArgs args)
        {
            Author = args.Author;
            LogMessage = args.LogMessage;
            Revision = args.Revision;
            Time = args.Time;
        }

        public string Author { get; set; }
        public string LogMessage { get; set; }
        public long Revision { get; set; }
        public DateTime Time { get; set; }
    }
}

