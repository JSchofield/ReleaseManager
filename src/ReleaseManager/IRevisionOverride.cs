namespace ReleaseManager
{
    using System;

    public interface IRevisionOverride
    {
        string Note { get; }
        long Revision { get; }
        bool CanBeReleased { get; set; }
        bool Ignore { get; set; }
        bool Breaking { get; set; }
        string SetBy { get; set; }
        DateTime SetTime { get; set; }
    }
}

