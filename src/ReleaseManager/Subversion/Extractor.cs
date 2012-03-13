using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ReleaseManager.Subversion
{
    public class Extractor
    {
        private readonly Regex expression;
        private readonly string group;

        public Extractor(string pattern, string groupToReturn)
        {
            this.expression = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            this.group = groupToReturn;
        }

        public IList<string> Find(string text)
        {
            MatchCollection matches = this.expression.Matches(text);
            IList<string> flags = new List<string>();
            foreach (Match match in matches)
            {
                foreach (Capture capture in match.Groups[this.group].Captures)
                {
                    if (!flags.Contains(capture.Value))
                    {
                        flags.Add(capture.Value);
                    }
                }
            }
            return flags;
        }
    }
}

