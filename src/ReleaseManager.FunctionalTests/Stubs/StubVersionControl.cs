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
            logItems = ReadLogFromFile(targetPath).Where(item => item.Revision >= startRevision && item.Revision <= (endRevision ?? long.MaxValue)).ToList();
            return true;
        }

        public bool GetInfo(string targetPath, out SvnInfoEventArgsWrapper info)
        {
            info = new SvnInfoEventArgsWrapper(ReadLogFromFile(targetPath).Max(w => w.Revision));
            return true;
        }

        public void Dispose()
        {
            
        }
    }
}
