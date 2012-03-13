using System;
using System.Collections.Generic;
using System.Globalization;
using NHibernate;
using NHibernate.Cfg;
using System.Linq;

namespace ReleaseManager.NHibernate
{
    public class NHibernateRepository : IEntityRepository, IDisposable
    {
        private static NHibernateRepository instance;
        private readonly ISessionFactory sessionFactory;

        public NHibernateRepository(): this(new Configuration().Configure())
        {
        }

        public NHibernateRepository(Configuration config)
        {
            this.sessionFactory = config.BuildSessionFactory();
        }

        public IRelease CreateRelease(string name)
        {
            return this.CreateRelease(name, null, null);
        }

        public IRelease CreateRelease(string name, string releaseManager, DateTime? releaseDate)
        {
            using (this.sessionFactory.OpenSession())
            {
                return new Release {
                    Name = name,
                    ReleaseManager = releaseManager,
                    ReleaseDate = releaseDate };
            }
        }

        public void DeleteRelease(IRelease release)
        {
            IList<IVersion> versions = this.GetVersionsInRelease(release.Name);

            using (ISession session = this.sessionFactory.OpenSession())
            {
                ITransaction tran = session.BeginTransaction();
                foreach (var version in versions)
                {
                    session.Delete(version);
                }
                session.Delete(release);
                tran.Commit();
            }
        }
        
        protected virtual void Dispose(bool all)
        {

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IComponent GetComponent(string componentName)
        {
            return GetComponentImpl(componentName);
        }

        private Component GetComponentImpl(string componentName)
        {
            const string query =
                @"select
                    c.Id,
                    c.Name,
                    c.Location,
                    c.Active,
                    (select max(SelectedRevision) from [Version] where ComponentId = c.Id) as HighestReleasedRevision
                from Component c
                where Name = '{0}'";

            using (ISession session = this.sessionFactory.OpenSession())
            {
                IList<Component> components =
                    session
                        .CreateSQLQuery(string.Format(CultureInfo.InvariantCulture, query, componentName))
                        .AddEntity("Component", typeof(Component))
                        .List<Component>();

                if (components.Count == 1)
                {
                    return components[0];
                }
                return null;
            }
        }

        public IList<IComponent> GetComponents()
        {
            const string query =
                @"select
                    c.Id,
                    c.Name,
                    c.Location,
                    c.Active
                from Component c";

            using (ISession session = this.sessionFactory.OpenSession())
            {
                return session
                    .CreateSQLQuery(query)
                    .AddEntity("Component", typeof(Component))
                    .List<Component>()
                    .ToList<IComponent>();
            }
        }
        
        public IRelease GetRelease(string releaseName)
        {
            return this.GetReleaseImpl(releaseName);
        }

        private Release GetReleaseImpl(string releaseName)
        {
            const string query =
                @"select
                    Id, 
                    Name, 
                    ReleaseManager, 
                    ReleaseDate 
                from 
                    Release
                where
                    Name ='{0}'";
            
            using (ISession session = this.sessionFactory.OpenSession())
            {
                IList<Release> releases = session
                    .CreateSQLQuery(string.Format(CultureInfo.InvariantCulture, query, releaseName))
                    .AddEntity("Release", typeof(Release))
                    .List<Release>();

                if (releases.Count == 1)
                {
                    return releases[0];
                }
                return null;
            }
        }

        public IList<IRelease> GetReleases()
        {
            const string query =
                @"select
                    Id, 
                    Name, 
                    ReleaseManager, 
                    ReleaseDate 
                from 
                    Release";

            using (ISession session = this.sessionFactory.OpenSession())
            {
                return session
                    .CreateSQLQuery(query)
                    .AddEntity("Release", typeof(Release))
                    .List<Release>()
                    .Cast<IRelease>()
                    .ToList();
            }
        }

        public IVersion GetVersion(string releaseName, string componentName)
        {
            using (ISession session = this.sessionFactory.OpenSession())
            {
                const string query =
                    @"select
                        v.Id, 
                        v.ReleaseId, 
                        v.ComponentId, 
                        v.StartRevision, 
                        v.EndRevision,
                        v.SelectedRevision
                    from
                        Version v 
                        join Release r
                            on v.ReleaseId = r.Id 
                        join Component c 
                            on v.ComponentId = c.Id
                    where
                        r.Name = '{0}'
                        and c.Name = '{1}'";
                
                IList<IVersion> versions = 
                    session
                        .CreateSQLQuery(string.Format(
                            CultureInfo.InvariantCulture, 
                            query, 
                            releaseName,
                            componentName))
                        .AddEntity("Version", typeof(Version))
                        .List<IVersion>();

                if (versions.Count == 1)
                {
                    return versions[0];
                }
                return null;
            }
        }

        public IList<IVersion> GetVersionsInRelease(string releaseName)
        {
            using (ISession session = this.sessionFactory.OpenSession())
            {
                const string query = 
                    @"select
                        v.Id, 
                        v.ReleaseId,
                        v.ComponentId,
                        v.StartRevision,
                        v.EndRevision,
                        v.SelectedRevision
                    from
                        Version v
                        join Release r
                            on v.ReleaseId = r.Id
                    where r.Name = '{0}'";

                return session
                    .CreateSQLQuery(string.Format(CultureInfo.InvariantCulture, query, releaseName))
                    .AddEntity("Version", typeof(Version))
                    .List<IVersion>();
            }
        }

        public IList<IVersion> GetVersionsOfComponent(string componentName)
        {
            using (ISession session = this.sessionFactory.OpenSession())
            {
                const string query =
                    @"select
                        v.Id, 
                        v.ReleaseId,
                        v.ComponentId,
                        v.StartRevision,
                        v.EndRevision,
                        v.SelectedRevision
                    from
                        Version v
                        join Component c
                            on v.ComponentId = c.Id
                    where c.Name = '{0}'";

                return session
                    .CreateSQLQuery(string.Format(CultureInfo.InvariantCulture, query, componentName))
                    .AddEntity("Version", typeof(Version))
                    .List<IVersion>();
            }
        }

        public void SaveComponent(IComponent component)
        {
            using (ISession session = this.sessionFactory.OpenSession())
            {
                ITransaction tran = session.BeginTransaction();
                session.SaveOrUpdate(component);
                tran.Commit();
            }
        }

        public void SaveRelease(IRelease release)
        {
            using (ISession session = this.sessionFactory.OpenSession())
            {
                ITransaction tran = session.BeginTransaction();
                session.SaveOrUpdate(release);
                tran.Commit();
            }
        }

        public void SaveVersion(IVersion version)
        {
            using (ISession session = this.sessionFactory.OpenSession())
            {
                ITransaction tran = session.BeginTransaction();
                session.SaveOrUpdate(version);
                tran.Commit();
            }
        }

        public static IEntityRepository Instance
        {
            get { return instance ?? (instance = new NHibernateRepository()); }
        }

        public void DeleteVersion(IVersion version)
        {
            using (ISession session = this.sessionFactory.OpenSession())
            {
                ITransaction tran = session.BeginTransaction();
                session.Delete(version);
                tran.Commit();
            }
        }

        public void DeleteComponent(IComponent component)
        {
            IList<IVersion> versions = this.GetVersionsOfComponent(component.Name);

            using (ISession session = this.sessionFactory.OpenSession())
            {
                ITransaction tran = session.BeginTransaction();
                foreach (var version in versions)
                {
                    session.Delete(version);
                }
                session.Delete(component);
                tran.Commit();
            }
        }

        public IVersion CreateVersion(
            string releaseName,
            string componentName,
            long startRevision,
            long? endRevision)
        {
            Component component = this.GetComponentImpl(componentName);
            Release release = this.GetReleaseImpl(releaseName);

            return 
                new Version {
                    Id = 0,
                    Component = component,
                    Release = release,
                    EndRevision = endRevision,
                    StartRevision = startRevision};
        }

        public IComponent CreateComponent(string name, string location)
        {
            return
                new Component {
                    Name = name,
                    Location = location,
                    Active = true};
        }
    }
}