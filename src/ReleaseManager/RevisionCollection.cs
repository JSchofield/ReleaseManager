using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ReleaseManager
{
    public class RevisionCollection : IRevisionCollection
    {
        private readonly List<IRevisionSetItem> revisions = new List<IRevisionSetItem>();

        public RevisionCollection(IEnumerable<IRevision> revisions)
        {
            bool previousCanBeReleased = true;
            foreach(IRevision revision in revisions.OrderBy(r => r.Number))
            {
                previousCanBeReleased = previousCanBeReleased && revision.ReleaseStatus;

                var item = new RevisionSetItem(revision, previousCanBeReleased);
               
                this.revisions.Add(item);
            }
        }

        public IList<IIssue> AllIssues
        {
            get
            {
                IList<IIssue> issues = new List<IIssue>();
                foreach (IRevisionSetItem revision in this.revisions)
                {
                    foreach (IIssue issue in revision.Issues)
                    {
                        if (!issues.Contains(issue))
                        {
                            issues.Add(issue);
                        }
                    }
                }
                return issues;
            }
        }
        
        public IRevisionSetItem BlockingRevision
        {
            get
            {
                // Lowest revision which is not ready and which has higher revisions which are ready.
                bool thereAreBlockers = false;
                IRevisionSetItem lowestUnreadySolution = null;
                foreach (IRevisionSetItem revision in this.OrderByDescending(r=>r.Number))
                {
                    if (!revision.CanBeReleased)
                    {
                        lowestUnreadySolution = revision;
                    }
                    if (revision.IsBlocked)
                    {
                        thereAreBlockers = true;
                    }
                }
                if (thereAreBlockers && (lowestUnreadySolution != this.LastOrDefault()))
                {
                    return lowestUnreadySolution;
                }
                return null;
            }
        }

        public IRevisionSetItem HighestReleasableRevision
        {
            get
            {
                IRevisionSetItem highest = null;
                foreach (RevisionSetItem revision in this.OrderBy(r=>r.Number))
                {
                    if (revision.CanBeReleased)
                    {
                        highest = revision;
                    }
                }
                return highest;
            }
        }

        public int ReleasableCommits
        {
            get
            {
                int count = 0;
                foreach (RevisionSetItem revision in this.OrderBy(r => r.Number))
                {
                    if (!revision.CanBeReleased)
                    {
                        return count;
                    }
                    count++;
                }
                return count;
            }
        }

        public IEnumerator<IRevisionSetItem> GetEnumerator()
        {
            return this.revisions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.revisions.GetEnumerator();
        }
    }
}

