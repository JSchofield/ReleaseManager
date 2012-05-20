using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReleaseManager.Subversion;
using System.IO;

namespace ReleaseManager.FunctionalTests.Stubs
{
    public class StubVersionControl : IVersionControlRepository
    {
        private IList<ILogItem> ReadLogFromFile(string path)
        {
            var log = new List<ILogItem>();
            using (var fileReader = new StreamReader(path))
            {
                string line;
                while ((line = fileReader.ReadLine()) != null)
                {
                    var fields = line.Split('\t');
                    log.Add(new LogItem {
                        Author = fields[2],
                        Revision = int.Parse(fields[0]),
                        Time = DateTime.Parse(fields[1]),
                        Message = fields[3] } );
                }
            }
            return log;
        }

        public IEnumerable<ILogItem> GetLogItems(string target, long startRevision, long? endRevision)
        {
            var targetUri = new Uri(target);
            if (!targetUri.IsFile)
            {
                throw new ArgumentException("Target must be a file URI");
            }
            var filePath = targetUri.LocalPath;

            var logItems = ReadLogFromFile(filePath);
            return logItems.Where(item => item.Revision >= startRevision && item.Revision <= (endRevision ?? long.MaxValue));
        }

        public IEnumerable<ILogItem> GetLogItems(Uri target, long startRevision, long? endRevision)
        {
            return GetLogItems(target.ToString(), startRevision, endRevision);
        }

        public long GetLastChangeRevision(string target)
        {
            var logItems = ReadLogFromFile(target);
            return logItems.Max(item => item.Revision);
        }

        public long GetLastChangeRevision(Uri target)
        {
            return GetLastChangeRevision(target.ToString());
        }
    }
}
