using System.Collections.Generic;

namespace ReleaseManager.Subversion
{
    public static class TicketExtractor
    {
        private static readonly Extractor extractor = new Extractor(@"\b(?<ticket>\w+\-\d+)\b", "ticket");

        public static IList<string> Find(string text)
        {
            return extractor.Find(text);
        }
    }
}

