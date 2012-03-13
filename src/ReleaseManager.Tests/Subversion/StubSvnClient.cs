using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReleaseManager.Subversion;

namespace ReleaseManager.Tests.Subversion
{
    public class StubSvnClient : ISvnClient
    {
        private readonly List<StubLogEvent> logEvents;
        private int revisionId;

        private string DefaultLocation { get; set; }

        public void AddEvent(DateTime time, string author, string location, string message)
        {
            logEvents.Add(
                new StubLogEvent(location, 
                    new SvnLogEventArgsWrapper() {
                        Revision = revisionId += 1,
                        Time = time,
                        Author = author,
                        LogMessage = message}));
        }

        public StubSvnClient()
        {
            logEvents = new List<StubLogEvent>();
            revisionId = 0;
        }

        public bool GetLog(string targetPath, long startRevision, long? endRevision, out ICollection<SvnLogEventArgsWrapper> logItems)
        {
            Predicate<StubLogEvent> predicate = le =>
                    le.Location.StartsWith(targetPath)
                    && le.LogEvent.Revision >= startRevision
                    && (!endRevision.HasValue || le.LogEvent.Revision <= endRevision);
 
            logItems = logEvents.FindAll(predicate).Select(le => le.LogEvent).ToList();

            return true;
        }

        public bool GetLog(Uri target, long startRevision, long? endRevision, out ICollection<SvnLogEventArgsWrapper> logItems)
        {
            return GetLog(target.ToString(), startRevision, endRevision, out logItems);
        }

        public bool GetInfo(string targetPath, out SvnInfoEventArgsWrapper info)
        {
            var revisions = logEvents.FindAll(le => le.Location.StartsWith(targetPath)).Select(le => le.LogEvent.Revision);
            long maxRevision = revisions.Count() == 0 ? 0 :revisions.Max();
            info = new SvnInfoEventArgsWrapper(maxRevision);
            return true;
        }

        public bool GetInfo(Uri target, out SvnInfoEventArgsWrapper info)
        {
            return GetInfo(target.ToString(), out info);
        }

        public void Dispose() {}

        private class StubLogEvent
        {
            public StubLogEvent(string location, SvnLogEventArgsWrapper logEvent)
            {
                Location = location;
                LogEvent = logEvent;
            }

            public string Location { get; private set; }
            public SvnLogEventArgsWrapper LogEvent { get; private set; }
        }
    }
}
