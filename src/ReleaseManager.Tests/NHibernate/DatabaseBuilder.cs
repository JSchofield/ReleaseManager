namespace ReleaseManager.Tests.Persistence
{
    using NUnit.Framework;
    using global::NHibernate;
    using global::NHibernate.Cfg;
    using global::NHibernate.Tool;
    using ReleaseManager.NHibernate;

    public static class SQLiteDatabase
    {
        static public IEntityRepository CreateInMemoryRepository()
        {
            Configuration config = InitialiseDatabase();
            return new NHibernateRepository(config);
        }

        private static Configuration InitialiseDatabase()
        {
            Configuration config = 
                new Configuration()
                    .SetProperty(Environment.Dialect, "NHibernate.Dialect.SQLiteDialect")
                    .SetProperty(Environment.ConnectionString, @"Data Source=C:\Users\jon\ReleaseManager\nhibernate.db;Version=3")
                    .SetProperty(Environment.ConnectionDriver, "NHibernate.Driver.SQLiteDriver")
                    .SetProperty(Environment.ConnectionProvider, "NHibernate.Connection.DriverConnectionProvider")
                    .AddAssembly(typeof(Component).Assembly);
            
            new global::NHibernate.Tool.hbm2ddl.SchemaExport(config).Execute(true, true, false);
            
            return config;
        }
    }

    public static class SqlServerDatabase
    {
        static public IEntityRepository CreateLocalReleaseManagerDb()
        {
            Configuration config = InitialiseDatabase();
            return new NHibernateRepository(config);
        }

        private static Configuration InitialiseDatabase()
        {
            Configuration config =
                new Configuration()
                    .SetProperty(Environment.Dialect, "NHibernate.Dialect.MsSql2005Dialect")
                    .SetProperty(Environment.ConnectionString, "Server=(local);initial catalog=ReleaseManager;Integrated Security=SSPI")
                    .SetProperty(Environment.ConnectionDriver, "NHibernate.Driver.SqlClientDriver")
                    .SetProperty(Environment.ConnectionProvider, "NHibernate.Connection.DriverConnectionProvider")
                    .AddAssembly(typeof(Component).Assembly);

            new global::NHibernate.Tool.hbm2ddl.SchemaExport(config).Execute(true, true, false);

            return config;
        }
    }

    [TestFixture]
    public class CreateDatabase
    {
        [Test]
        public void Create()
        {
            var testRepo = SqlServerDatabase.CreateLocalReleaseManagerDb();
        }
    }

    [TestFixture]
    public class TestDatabaseBuilder
    {
        [Test]
        public void CreateDatabaseObjects()
        {
            var testRepo = SQLiteDatabase.CreateInMemoryRepository();

            testRepo.SaveComponent(new Component { Name = "Test", Location = "svn:\\repo\test", Active = true });

            IComponent comp = testRepo.GetComponent("Test");

            Assert.NotNull(comp);
            Assert.AreEqual("Test", comp.Name);
        }

    }
}
