namespace ReleaseManager.NHibernate
{
    public class Component : IComponent
    {
        public virtual int Id { get; protected set; }
        public virtual bool Active { get; set; }
        public virtual string Location {get; set; }
        public virtual string Name { get; set; }
    }
}

