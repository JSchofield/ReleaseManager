using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;

namespace ReleaseManager.Subversion
{
    public class SvnRepository : IVersionControlRepository, IDisposable
    {
        private readonly ISvnClient client;
        private readonly IDictionary<GetLogItemsArgs, IEnumerable<ILogItem>> cache = new Dictionary<GetLogItemsArgs, IEnumerable<ILogItem>>();

        public SvnRepository(ISvnClient client)
        {
            this.client = client;
        }

        public SvnRepository(string userName, string password)
            : this(new SvnClientWrapper(userName, password))
        {}

        public SvnRepository()
            : this(SvnConfig.GetConfiguration())
        {}

        public SvnRepository(Configuration config)
            : this(SvnConfig.GetConfiguration(config))
        {}

        public SvnRepository(ISvnConfig config)
            : this(config.UserName, config.Password)
        {}

        public static ILogItem CreateLogItem(SvnLogEventArgsWrapper logEvent)
        {
            var logItem = new LogItem {
                Author = logEvent.Author,
                Message = logEvent.LogMessage,
                Time = logEvent.Time,
                Revision = logEvent.Revision };

            foreach(var directive in DirectiveExtractor.Find(logEvent.LogMessage))
            {
                logItem.Directives.Add(directive);
            }
            foreach(var ticket in TicketExtractor.Find(logEvent.LogMessage))
            {
                logItem.Tickets.Add(ticket);
            }

            return logItem;
        }

        protected virtual void Dispose(bool all)
        {
            this.client.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public long GetLastChangeRevision(string target)
        {
            return GetLastChangeRevision(new Uri(target));
        }

        public long GetLastChangeRevision(Uri target)
        {
            SvnInfoEventArgsWrapper result;
            this.client.GetInfo(target, out result);
            return result.LastChangeRevision;
        }
        
        public IEnumerable<ILogItem> GetLogItems(string target, long startRevision, long? endRevision)
        {
            return this.GetLogItems(new Uri(target), startRevision, endRevision);
        }

        public IEnumerable<ILogItem> GetLogItems(Uri target, long startRevision, long? endRevision)
        {
            var key =
                new GetLogItemsArgs {
                    Target = target, 
                    StartRevision = startRevision, 
                    EndRevision = endRevision};

            IEnumerable<ILogItem> result;
            if (cache.ContainsKey(key))
            {
                result = cache[key];
            }
            else
            {
                result = GetLogItemsFromSvn(target, startRevision, endRevision);
                cache.Add(key, result);
            }
            return result;
        }

        private IEnumerable<ILogItem> GetLogItemsFromSvn(Uri target, long startRevision, long? endRevision)
        {
            var logItems = new Collection<ILogItem>();
            if (startRevision <= this.GetLastChangeRevision(target))
            {
                ICollection<SvnLogEventArgsWrapper> logEvents;
                if (!(this.client.GetLog(target, startRevision, endRevision, out logEvents) && (logEvents != null)))
                {
                    throw new SvnGetLogException("No items retrieved from Subversion.");
                }
                foreach (SvnLogEventArgsWrapper logEvent in logEvents)
                {
                    logItems.Add(CreateLogItem(logEvent));
                }
            }
            return logItems;
        }
    }
}

