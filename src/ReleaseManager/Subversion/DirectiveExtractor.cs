using System.Collections.Generic;

namespace ReleaseManager.Subversion
{
    public static class DirectiveExtractor
    {
        private static readonly Extractor extractor = new Extractor(@"(?<=^|\W)\$(?<flag>\w+)\b", "flag");

        public static IList<string> Find(string text)
        {
            return extractor.Find(text);
        }
    }
}

