using ReleaseManager.Tests.Persistence;
using ReleaseManager.Tests.Subversion;
using ReleaseManager.Subversion;
using ReleaseManager.Tests.Jira;

namespace ReleaseManager.Tests
{
    public static class MasterRepositoryFactory
    {
        public static MasterRepository CreateStubRepo()
        { 
            var repo = new MasterRepository(
                SQLiteDatabase.CreateInMemoryRepository(),
                new SvnRepository(new StubSvnClient()),
                InMemoryIssueRepository.CreateIssueRepository());

            return repo;
        }
    }
}
