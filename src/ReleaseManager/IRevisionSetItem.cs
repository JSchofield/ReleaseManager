namespace ReleaseManager
{
    public interface IRevisionSetItem : IRevision
    {
        //bool PropogatedStatus { get; }
        bool CanBeReleased { get; }
        bool IsBlocked { get; }
    }
}

