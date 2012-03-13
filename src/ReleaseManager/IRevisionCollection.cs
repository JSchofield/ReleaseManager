using System.Collections.Generic;

namespace ReleaseManager
{
    public interface IRevisionCollection : IEnumerable<IRevisionSetItem>
    {
        IList<IIssue> AllIssues { get; }

        IRevisionSetItem BlockingRevision { get; }

        IRevisionSetItem HighestReleasableRevision { get; }

        int ReleasableCommits { get; }
    }
}

