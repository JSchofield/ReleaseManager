using System;
using System.Collections.Generic;

namespace ReleaseManager.Subversion
{
    // This interface duplicates the required parts of the SharpSvn SvnClient to enable mocking
    public interface ISvnClient : IDisposable
    {
        bool GetLog(string targetPath, long startRevision, long? endRevision, out ICollection<SvnLogEventArgsWrapper> logItems);
        bool GetLog(Uri target, long startRevision, long? endRevision, out ICollection<SvnLogEventArgsWrapper> logItems);
        bool GetInfo(string targetPath, out SvnInfoEventArgsWrapper info);
        bool GetInfo(Uri target, out SvnInfoEventArgsWrapper info);
    }
}

