namespace ReleaseManager.Jira
{
    public interface IStatusMap
    {
        bool this[string status] { get; }
    }
}