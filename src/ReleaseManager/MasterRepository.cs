using System;
using System.Collections.Generic;
using System.Linq;

namespace ReleaseManager
{
    public class MasterRepository
    {
        private readonly IRevisionSetBuilder builder;
        private readonly IEntityRepository entityRepo;

        public MasterRepository(
            IEntityRepository entityRepo,
            IVersionControlRepository vcRepo, 
            IIssueRepository issueRepo)
        {
            this.entityRepo = entityRepo;
            builder = new RevisionSetBuilder(vcRepo, issueRepo);
        }

        public IComponent GetComponent(string componentName)
        {
            return this.entityRepo.GetComponent(componentName);
        }

        public IList<IComponent> GetComponents()
        {
            return this.entityRepo.GetComponents();
        }

        public IRelease GetRelease(string releaseName)
        {
            return this.entityRepo.GetRelease(releaseName);
        }

        public IList<IRelease> GetReleases()
        {
            return this.entityRepo.GetReleases();
        }

        public IVersionWork GetVersion(string releaseName, string componentName)
        {
            return new VersionWork(this.entityRepo.GetVersion(releaseName, componentName), this.builder);
        }

        public IList<IVersionWork> GetVersionsInRelease(string releaseName)
        {
            return this.entityRepo.GetVersionsInRelease(releaseName).Select(v => new VersionWork(v, this.builder)).Cast<IVersionWork>().ToList();
        }

        public IList<IVersionWork> GetVersionsOfComponent(string componentName)
        {
            return this.entityRepo.GetVersionsOfComponent(componentName).Select(v => new VersionWork(v, this.builder)).Cast<IVersionWork>().ToList();
        }

        public void SaveRelease(IRelease release)
        {
            this.entityRepo.SaveRelease(release);
        }

        public void SaveVersion(IVersion version)
        {
            this.entityRepo.SaveVersion(version);
        }

        public void DeleteVersion(IVersion version)
        {
            this.entityRepo.DeleteVersion(version);
        }

        public IVersion CreateVersion(
            string releaseName, 
            string componentName, 
            long startRevision, 
            long? endRevision)
        {
            return this.entityRepo.CreateVersion(releaseName, componentName, startRevision, endRevision);
        }

        public IRelease CreateRelease(string name)
        {
            return this.entityRepo.CreateRelease(name);
        }

        public IRelease CreateRelease(string name, string releaseManager, DateTime? releaseDate)
        {
            return this.entityRepo.CreateRelease(name, releaseManager, releaseDate);
        }

        public void DeleteRelease(IRelease release)
        {
            this.entityRepo.DeleteRelease(release);
        }

        public IComponent CreateComponent(string name, string location)
        {
            return this.entityRepo.CreateComponent(name, location);
        }

        public void SaveComponent(IComponent component)
        {
            this.entityRepo.SaveComponent(component);
        }

        /*
        public void DeleteComponent(IComponent component)
        {
            this.entityRepo.DeleteComponent(component);
        }
        */
    }
}
