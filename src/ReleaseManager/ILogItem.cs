namespace ReleaseManager
{
    using System;
    using System.Collections.Generic;

    public interface ILogItem
    {
        string Author { get; }

        IList<string> Directives { get; }

        string Message { get; }

        long Revision { get; }

        IList<string> Tickets { get; }

        DateTime Time { get; }
    }
}

