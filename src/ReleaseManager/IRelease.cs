namespace ReleaseManager
{
    using System;

    public interface IRelease
    {
        string Name { get; set; }

        DateTime? ReleaseDate { get; set; }

        string ReleaseManager { get; set; }
    }
}

