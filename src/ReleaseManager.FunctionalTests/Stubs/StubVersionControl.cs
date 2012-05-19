using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReleaseManager.Subversion;

namespace ReleaseManager.FunctionalTests.Stubs
{
    public class StubVersionControl : IVersionControlRepository
    {
        public IEnumerable<ILogItem> GetLogItems(string target, long startRevision, long? endRevision)
        {
            return new List<ILogItem> { 
                new LogItem() { Author = "jonathon", Revision = 1, Time = DateTime.Parse("2012-03-21 15:30:00"), Message = "Commit 1" },
                new LogItem() { Author = "jonathon", Revision = 2, Time = DateTime.Parse("2012-03-21 15:31:00"), Message = "Commit 2" },
                new LogItem() { Author = "jonathon", Revision = 3, Time = DateTime.Parse("2012-03-21 15:32:00"), Message = "Commit 3" } };
        }

        public IEnumerable<ILogItem> GetLogItems(Uri target, long startRevision, long? endRevision)
        {
            return new List<ILogItem> { 
                new LogItem() { Author = "jonathon", Revision = 1, Time = DateTime.Parse("2012-03-21 15:30:00"), Message = "Commit 1" },
                new LogItem() { Author = "jonathon", Revision = 2, Time = DateTime.Parse("2012-03-21 15:31:00"), Message = "Commit 2" },
                new LogItem() { Author = "jonathon", Revision = 3, Time = DateTime.Parse("2012-03-21 15:32:00"), Message = "Commit 3" } };
        }

        public long GetLastChangeRevision(Uri target)
        {
            return 3;
        }

        public long GetLastChangeRevision(string target)
        {
            return 3;
        }
    }
}
