using System;
using System.IO;
using System.Collections.Generic;
using ReleaseManager.Jira;

namespace ReleaseManager.Tests.Jira
{
    public class StubStatusMap: IStatusMap
    {
        private IDictionary<string, bool> _statusMap;

        public StubStatusMap(string path)
        {
            _statusMap = LoadStatusMapFromFile(path);
        }

        public IDictionary<string, bool> LoadStatusMapFromFile(string statusesFile)
        {
            var map = new Dictionary<string, bool>();
            using (var fileReader = new StreamReader(statusesFile))
            {
                string line;
                while ((line = fileReader.ReadLine()) != null)
                {
                    var fields = line.Split('\t');
                    map.Add(fields[0], bool.Parse(fields[1]));
                }
            }
            return map;
        }

        public bool this[string status]
        {
            get { return _statusMap[status]; }
        }
    }
}
