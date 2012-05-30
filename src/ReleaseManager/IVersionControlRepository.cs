using System;
using System.Collections.Generic;

namespace ReleaseManager
{
    public interface IVersionControlRepository
    {
        IEnumerable<ILogItem> GetLogItems(string target, long startRevision, long? endRevision);
        long GetLastChangeRevision(string target);
    }
}