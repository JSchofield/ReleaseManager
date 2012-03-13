namespace ReleaseManager.Web
{
    using Castle.Core.Resource;
    using Castle.Windsor;
    using Castle.Windsor.Configuration.Interpreters;
    using ReleaseManager.Jira;
    using ReleaseManager.Tests;

    public class Core
    {
        public static MasterRepository Repo { get; private set; }

        static Core()
        {
            Repo = MasterRepositoryFactory.CreateStubRepo();
            //IWindsorContainer container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));
            //Repo = (MasterRepository)container["master.repository"];
        }

        static public void FlushJiraCache()
        {
            JiraRepository.FlushCache();
        }
    }
}