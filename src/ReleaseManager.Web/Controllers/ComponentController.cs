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

        public ActionResult Save()
        {
            var components = Core.Repo.GetComponents().Select(c => new ComponentViewModel(c));
            return View("List", components);
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
