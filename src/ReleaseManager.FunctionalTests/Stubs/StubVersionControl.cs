using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReleaseManager.Subversion;
using System.IO;

namespace ReleaseManager.FunctionalTests.Stubs
{
    public class StubVersionControl : ISvnClient
    {
        private IList<SvnLogEventArgsWrapper> ReadLogFromFile(string path)
        {
            var log = new List<SvnLogEventArgsWrapper>();
            using (var fileReader = new StreamReader(path))
            {
                string line;
                while ((line = fileReader.ReadLine()) != null)
                {
                    var fields = line.Split('\t');
                    log.Add(new SvnLogEventArgsWrapper {
                        Revision = int.Parse(fields[0]),
                        Time = DateTime.Parse(fields[1]),
                        Author = fields[2],
                        LogMessage = fields[3] } );
                }
            }
            return log;
        }

        public bool GetLog(string targetPath, long startRevision, long? endRevision, out ICollection<SvnLogEventArgsWrapper> logItems)
        {
            var targetUri = new Uri(targetPath);
            if (!targetUri.IsFile)
            {
                throw new ArgumentException("Target must be a file URI");
            }
            var filePath = targetUri.LocalPath;

            logItems = ReadLogFromFile(filePath).Where(item => item.Revision >= startRevision && item.Revision <= (endRevision ?? long.MaxValue)).ToList();
            return true;
        }

        public bool GetLog(Uri target, long startRevision, long? endRevision, out ICollection<SvnLogEventArgsWrapper> logItems)
        {
            return GetLog(target.ToString(), startRevision, endRevision, out logItems);
        }

        public bool GetInfo(string targetPath, out SvnInfoEventArgsWrapper info)
        {
            var targetUri = new Uri(targetPath);
            if (!targetUri.IsFile)
            {
                throw new ArgumentException("Target must be a file URI");
            }
            var filePath = targetUri.LocalPath;

            info = new SvnInfoEventArgsWrapper(ReadLogFromFile(filePath).Max(w => w.Revision));
            return true;
        }

        public bool GetInfo(Uri target, out SvnInfoEventArgsWrapper info)
        {
            return GetInfo(target.ToString(), out info);
        }

        public void Dispose()
        {
            
        }
    }
}
