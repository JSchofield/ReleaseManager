namespace ReleaseManager
{
    public interface IRevisionSetBuilder
    {
        IRevisionCollection CreateRevisionSet(IVersion version);
    }
}

