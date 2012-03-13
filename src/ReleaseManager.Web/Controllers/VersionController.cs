namespace ReleaseManager.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ReleaseManager.Web.Models;

    public class VersionController : Controller
    {
        public ActionResult Detail(string releaseName, string componentName)
        {
            IVersionWork version = Core.Repo.GetVersion(releaseName, componentName);
            return View(new VersionViewModel(version));
        }

        public ActionResult Tickets(string releaseName, string componentName)
        {
            IVersionWork version = Core.Repo.GetVersion(releaseName, componentName);
            return View(new VersionViewModel(version));
        }

        public ActionResult Wip(string releaseName, string componentName)
        {
            IVersionWork version = Core.Repo.GetVersion(releaseName, componentName);
            return View(new VersionViewModel(version));
        }

        public ActionResult Save(
            string releaseName,
            string componentName,
            long? selected,
            IList<RevisionUpdateModel> revisionEdits)
        {
            IVersionWork version = Core.Repo.GetVersion(releaseName, componentName);
            version.SelectedRevision = selected;
            
            foreach(var edit in revisionEdits)
            {
                if ((edit.UserOverride == null 
                    || !edit.UserOverride.IsSet)
                    && version.RevisionOverrides.ContainsKey(edit.Number))
                {
                    version.RemoveOverride(edit.Number);
                }
                else if (edit.UserOverride != null && edit.UserOverride.IsSet)
                {
                    version.SetOverride(
                        edit.Number,
                        edit.UserOverride.CanBeReleased.Value,
                        edit.UserOverride.Ignore.Value,
                        "system");
                }
            }

            Core.Repo.SaveVersion(version.VersionData());

            return this.RedirectToAction("Detail", new {releaseName, componentName});
        }
    }
}