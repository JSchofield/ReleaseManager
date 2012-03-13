namespace ReleaseManager
{
    using System;

    public interface IIssue
    {
        string Assignee { get; }

        string Components { get; }

        string Database { get; }

        string Key { get; }

        Uri Link { get; }

        bool CanBeReleased { get; }

        string Status { get; }

        string Summary { get; }
    }
}

