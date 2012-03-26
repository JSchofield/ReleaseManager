namespace ReleaseManager
{
    public interface IComponent
    {
        bool Active { get; set; }
        string Location { get; set; }
        string Name { get; }
    }
}

