using System.Collections.Generic;

namespace ReleaseManager.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using ReleaseManager.Web.Models;

    public class ComponentController : Controller
    {
        //
        // GET: /Component/

        public ActionResult List()
        {
            var components = Core.Repo.GetComponents().Select(c => new ComponentViewModel(c)).ToList();
            return View(components);
        }

        public ActionResult Save(IList<ComponentViewModel> componentEdits)
        {
            var components = Core.Repo.GetComponents();

            foreach (var edit in componentEdits)
            {
                var current = components.FirstOrDefault(c => c.Name == edit.Name);
                if (current != null)
                {
                    current.Location = edit.Location;
                    current.Active = edit.Active;
                    Core.Repo.SaveComponent(current);
                }
            }

            return View("List",  Core.Repo.GetComponents().Select(c => new ComponentViewModel(c)).ToList());
        }

        public ActionResult New()
        {
            var newComponent = new ComponentViewModel();
            return View(newComponent);
        }

        public ActionResult Create(ComponentViewModel component)
        {
            IComponent newComponent = Core.Repo.CreateComponent(component.Name, component.Location);
            Core.Repo.SaveComponent(newComponent);
            return this.RedirectToRoute("ComponentList", new object());
        }
    }
}
