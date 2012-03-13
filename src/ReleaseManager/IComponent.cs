namespace ReleaseManager
{
    public interface IComponent
    {
        bool Active { get; set; }
        string Location { get; }
        string Name { get; }
    }
}

