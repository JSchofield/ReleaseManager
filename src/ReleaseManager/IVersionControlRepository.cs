using System;
using System.Collections.Generic;

namespace ReleaseManager
{
    public interface IVersionControlRepository
    {
        IEnumerable<ILogItem> GetLogItems(string target, long startRevision, long? endRevision);
        IEnumerable<ILogItem> GetLogItems(Uri target, long startRevision, long? endRevision);
        long GetLastChangeRevision(Uri target);
        long GetLastChangeRevision(string target);
    }
}