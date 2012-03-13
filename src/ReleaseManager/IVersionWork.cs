namespace ReleaseManager
{
    public interface IVersionWork: IVersion
    {
        IRevisionCollection Revisions { get; }
        IVersion VersionData();
    }
}