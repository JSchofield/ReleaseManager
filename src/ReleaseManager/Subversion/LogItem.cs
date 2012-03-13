using System;
using System.Collections.Generic;

namespace ReleaseManager.Subversion
{
    public class LogItem : ILogItem
    {
        public LogItem()
        {
            this.Tickets = new List<string>();
            this.Directives = new List<string>();
        }

        public string Author
        {
            get; set; }

        public IList<string> Directives
        {
            get; private set; }

        public string Message
        {
            get; set; }

        public long Revision
        {
            get; set; }

        public IList<string> Tickets
        {
            get; private set; }

        public DateTime Time
        {
            get; set; }
    }
}

