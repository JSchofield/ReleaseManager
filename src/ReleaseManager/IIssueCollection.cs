using System.Collections.Generic;

namespace ReleaseManager
{
    public interface IIssueCollection: IList<IIssue>
    {
        bool CanBeReleased { get; }
    }
}