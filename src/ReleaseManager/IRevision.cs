namespace ReleaseManager
{
    using System;

    public interface IRevision
    {
        long Number { get; }

        string Author { get; }

        DateTime Time { get; }

        string Message { get; }

        IIssueCollection Issues { get; }

        bool NaturalReleaseStatus { get; }

        bool NaturalIgnore { get; }

        bool ReleaseStatus { get; }

        bool Ignore { get; }
        
        IRevisionOverride UserOverride { get; set; }
    }
}