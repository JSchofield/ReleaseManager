using ReleaseManager.Jira;
using System.Collections.Generic;
using System.Linq;

namespace ReleaseManager
{
    public class RevisionSetBuilder : IRevisionSetBuilder
    {
        private readonly IIssueRepository issueRepo;
        private readonly IVersionControlRepository vcRepo;

        public RevisionSetBuilder(IVersionControlRepository vcRepo, IIssueRepository issueRepo)
        {
            this.vcRepo = vcRepo;
            this.issueRepo = issueRepo;
        }

        private static IRevision CreateRevision(ILogItem logItem, IssueCollection issues, IRevisionOverride revisionOverride)
        {
            return new Revision(logItem, revisionOverride, issues);
        }

        public IRevisionCollection CreateRevisionSet(IVersion version)
        {
            IEnumerable<ILogItem> logItems = this.GetLogItems(version);
            ICollection<IRevision> revisions = new List<IRevision>();
            foreach (ILogItem logItem in logItems)
            {
                IssueCollection issues = this.GetIssues(logItem.Tickets);
                IRevisionOverride overrride = GetOverride(version, logItem.Revision);
                revisions.Add(CreateRevision(logItem, issues, overrride));
            }

            var revisionCollection = new RevisionCollection(revisions);
            return revisionCollection;
        }

        private IssueCollection GetIssues(IEnumerable<string> ticketReferences)
        {
            return new IssueCollection(ticketReferences.Select(key => this.issueRepo.GetIssue(key)).ToList());
        }

        private IEnumerable<ILogItem> GetLogItems(IVersion version)
        {
            return this.vcRepo.GetLogItems(version.Component.Location, version.StartRevision, version.EndRevision);
        }

        private static IRevisionOverride GetOverride(IVersion release, long revision)
        {
            return release.RevisionOverrides.ContainsKey(revision) ? release.RevisionOverrides[revision] : null;
        }
    }
}

