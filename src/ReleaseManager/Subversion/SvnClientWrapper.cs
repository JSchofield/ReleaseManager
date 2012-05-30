using SharpSvn;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;

namespace ReleaseManager.Subversion
{
    public class SvnClientWrapper : ISvnClient
    {
        private readonly SvnClient client = new SvnClient();

        public SvnClientWrapper(string userName, string password)
        {
            this.client.Authentication.DefaultCredentials = new NetworkCredential(userName, password);
        }

        protected virtual void Dispose(bool all)
        {
            this.client.Dispose();
            
        }
        
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual bool GetInfo(string targetPath, out SvnInfoEventArgsWrapper info)
        {
            SvnInfoEventArgs svnInfo;
            bool result = this.client.GetInfo(new Uri(targetPath), out svnInfo);
            info = new SvnInfoEventArgsWrapper(svnInfo.LastChangeRevision);
            return result;
        }

        public virtual bool GetLog(string targetPath, long startRevision, long? endRevision, out ICollection<SvnLogEventArgsWrapper> logItems)
        {
            Collection<SvnLogEventArgs> svnLogItems;
            bool result = this.client.GetLog(new Uri(targetPath), GetLogArgs(startRevision, endRevision), out svnLogItems);
            logItems = svnLogItems.ToList().ConvertAll(item => new SvnLogEventArgsWrapper(item));
            return result;
        }

        private static SvnLogArgs GetLogArgs(long start, long? end)
        {
            var startRevision = new SvnRevision(start);
            var endRevision = end.HasValue ? new SvnRevision(end.Value) : SvnRevision.Head;
            return new SvnLogArgs(new SvnRevisionRange(startRevision, endRevision));
        }
    }
}

