using System;
using System.Collections.Generic;

namespace ReleaseManager
{
    public interface IEntityRepository
    {
        IList<IRelease> GetReleases();
        IRelease GetRelease(string releaseName);
        IRelease CreateRelease(string name);
        IRelease CreateRelease(string name, string releaseManager, DateTime? releaseDate);
        void SaveRelease(IRelease release);
        void DeleteRelease(IRelease release);

        IList<IComponent> GetComponents();
        IComponent GetComponent(string componentName);
        IComponent CreateComponent(string name, string location);
        void SaveComponent(IComponent component);
        void DeleteComponent(IComponent component);

        IVersion GetVersion(string releaseName, string componentName);
        IList<IVersion> GetVersionsInRelease(string releaseName);
        IList<IVersion> GetVersionsOfComponent(string componentName);
        IVersion CreateVersion(string releaseName, string componentName, long startRevision, long? endRevision);
        void SaveVersion(IVersion version);
        void DeleteVersion(IVersion version);

    }
}

