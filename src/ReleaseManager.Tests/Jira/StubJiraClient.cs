﻿using System.Linq;
using System.Collections.Generic;
using ReleaseManager.Jira;

namespace ReleaseManager.Tests.Jira
{
    public class StubJiraClient: IJiraSoapClient
    {
        private class RemoteStatus: IRemoteStatus
        {
            public string id { get; set;}
            public string name { get; set;}
        }

        private class RemoteIssue : IRemoteIssue
        {
            public string assignee { get; set; }
            public string key { get; set; }
            public string summary { get; set; }
            public string status { get; set; }
        }

        private readonly IRemoteStatus[] statuses;
        private readonly List<IRemoteIssue> issues;
        private string expectedToken;

        public StubJiraClient()
        {
            issues = new List<IRemoteIssue>();

            statuses = new IRemoteStatus[] {
            new RemoteStatus() { id = "Open", name = "Open" },
            new RemoteStatus() { id = "Closed", name = "Closed" },
            new RemoteStatus() { id = "In Dev", name = "In Dev" }};
        }

        public void AddIssue(string key, string assignee, string status, string summary)
        {
            var issue = new RemoteIssue()
            {
                key = key,
                assignee = assignee,
                status = status,
                summary = summary
            };

            this.issues.Add(issue);
        }

        public string login(string userName, string password)
        {
            expectedToken = "12345";
            return expectedToken;
        }

        public IRemoteStatus[] getStatuses(string token)
        {
            return statuses;
        }

        public IRemoteIssue getIssue(string token, string key)
        {
            return issues.FirstOrDefault(i => i.key == key);
        }

        public void Close()
        {
        }
    }
}