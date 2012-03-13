namespace ReleaseManager.Web.Models
{
    public class ComponentViewModel
    {
        public ComponentViewModel()
        {}

        public ComponentViewModel(IComponent component)
        {
            Active = component.Active;
            Location = component.Location;
            Name = component.Name;
        }

        public bool Active { get; set; }

        public string Location { get; set; }

        public string Name { get; set; }
    }
}