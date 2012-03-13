using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using ReleaseManager.Jira.SoapClient;

namespace ReleaseManager.Jira
{
    public class JiraRepository : IIssueRepository, IDisposable
    {
        private static readonly IDictionary<string, CachedItem> cache = new Dictionary<string, CachedItem>();
        private readonly TimeSpan cacheExpiryPeriod;
        private readonly Uri baseUri;
        private readonly IJiraSoapClient client;
        private readonly IRemoteStatus[] statuses;
        private readonly IStatusMap statusMap = new StatusMap();
        private readonly string token;

        public static void FlushCache()
        {
            cache.Clear();
        }

        public JiraRepository(IJiraSoapClient client, Uri baseUri, TimeSpan cacheExpiryPeriod, IStatusMap statusMap)
        {
            this.client = client;
            this.baseUri = baseUri;
            this.cacheExpiryPeriod = cacheExpiryPeriod;
            this.token = this.client.login("name", "password");
            this.statuses = this.client.getStatuses(this.token);
            this.statusMap = statusMap;
        }

        public JiraRepository(TimeSpan cacheExpiryPeriod, Uri baseUri, IStatusMap statusMap)
            : this(new JiraSoapServiceClient(), baseUri, cacheExpiryPeriod, statusMap)
        {}

        public JiraRepository(
            Uri baseUri, 
            string userName,
            string password, 
            TimeSpan cacheExpiryPeriod,
            IStatusMap statusMap)
        {
            this.baseUri = baseUri;
            this.cacheExpiryPeriod = cacheExpiryPeriod;
            this.token = this.client.login(userName, password);
            this.statuses = this.client.getStatuses(this.token);
            this.statusMap = statusMap;
        }

        public JiraRepository(Configuration config)
            : this(JiraConfig.GetConfiguration(config))
        {}

        public JiraRepository()
            : this(JiraConfig.GetConfiguration())
        {}

        public JiraRepository(IJiraConfig config)
            : this(config.BaseUri, config.UserName, config.Password, config.CacheExpireTime, config.StatusMap)
        {}

        public Issue CreateIssue(IRemoteIssue remoteIssue)
        {
            string statusName = GetStatusName(remoteIssue.status, this.statuses);
            var issue = new Issue {
                Status = statusName,
                Key = remoteIssue.key,
                Assignee = remoteIssue.assignee,
                CanBeReleased = this.statusMap[statusName],
                Summary = remoteIssue.summary,
                Link = new Uri(baseUri, new Uri("browse/" + remoteIssue.key, UriKind.Relative))};
            return issue;
        }

        public Issue CreateNotFoundIssue(string key)
        {
            var issue = new Issue {
                Status = "Not found",
                Key = key,
                CanBeReleased = false,
                Link = new Uri(baseUri, new Uri("browse/" + key, UriKind.Relative))};

            return issue;
        }
        
        public IIssue GetIssue(string key)
        {
            IIssue issue;
            if (cache.ContainsKey(key))
            {
                CachedItem item = cache[key];
                if ((DateTime.Now - item.TimeCached) > this.cacheExpiryPeriod)
                {
                    issue = this.GetIssueFromService(key);
                    cache[key] = new CachedItem(issue);
                    return issue;
                }
                return cache[key].Issue;
            }
            issue = this.GetIssueFromService(key);
            cache.Add(key, new CachedItem(issue));
            return issue;
        }

        public IIssue GetIssueFromService(string key)
        {
            IRemoteIssue remoteIssue;
            //try
            //{
                remoteIssue = this.client.getIssue(this.token, key);
            //}
            //catch (Exception)
            //{
            //    remoteIssue = null;
            //}
            if (remoteIssue == null)
            {
                return this.CreateNotFoundIssue(key);
            }
            return this.CreateIssue(remoteIssue);
        }

        static private string GetStatusName(string statusId, IEnumerable<IRemoteStatus> statuses)
        {
            IRemoteStatus status = statuses.FirstOrDefault(s => s.id == statusId);
            if (status != null)
            {
                return status.name;
            }
            return "Unknown";
        }

        private class CachedItem
        {
            public readonly IIssue Issue;
            public readonly DateTime TimeCached;

            public CachedItem(IIssue issue)
            {
                this.Issue = issue;
                this.TimeCached = DateTime.Now;
            }
        }

        protected virtual void Dispose(bool all)
        {
            this.client.Close();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}