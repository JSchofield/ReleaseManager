namespace ReleaseManager.Web.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using ReleaseManager.Web.Models;

    public class ReleaseController : Controller
    {
        public ActionResult New()
        {
            IList<IComponent> components = Core.Repo.GetComponents();
            IList<ReleaseComponentViewModel> list =
                components.Select(c => new ReleaseComponentViewModel(c.Name, null, GetSuggestedNextStartRevision(c.Name))).ToList();

            return View(new ReleaseUpdateModel(list));
        }

        public long GetSuggestedNextStartRevision(string componentName)
        {
            IList<IVersionWork> versions = Core.Repo.GetVersionsOfComponent(componentName);
            long? maxSelectedRevision = versions.Max(v => v.SelectedRevision);
            long? maxEndRevision = versions.Max(v => v.EndRevision);
            return ((maxSelectedRevision ?? maxEndRevision) ?? 0) + 1;
        }

        public ActionResult Edit(string releaseName)
        {
            IRelease release = Core.Repo.GetRelease(releaseName);
            IList<IComponent> components = Core.Repo.GetComponents();
            IList<IVersionWork> versions = Core.Repo.GetVersionsInRelease(releaseName);

            IList<ReleaseComponentViewModel> list = 
                components.Select(
                    c => new ReleaseComponentViewModel(
                        c.Name, 
                        versions.FirstOrDefault(
                            v => v.Component.Name == c.Name),
                             GetSuggestedNextStartRevision(c.Name))).ToList();

            return View(new ReleaseUpdateModel(release, list));
        }

        
        public ActionResult Create(ReleaseUpdateModel releaseEdits)
        {
            var release = Core.Repo.CreateRelease(
                releaseEdits.Name, 
                releaseEdits.ReleaseManager,
                DateTime.Parse(releaseEdits.ReleaseDate, CultureInfo.CurrentCulture));

            Core.Repo.SaveRelease(release);

            foreach (ReleaseComponentViewModel component in releaseEdits.Components)
            {
                if (component.Included)
                {
                    var newVersion = Core.Repo.CreateVersion(
                        releaseEdits.Name,
                        component.Name,
                        long.Parse(component.StartRevision, CultureInfo.InvariantCulture),
                        ParseRevisionText(component.EndRevision));
                    newVersion.SelectedRevision = ParseRevisionText(component.SelectedRevision);
                    Core.Repo.SaveVersion(newVersion);
                }
            }
            return RedirectToRoute("Release", new { releaseName = releaseEdits.Name, action = "Edit" });
        }

        public ActionResult Save(ReleaseUpdateModel releaseEdits)
        {
            IRelease release = Core.Repo.GetRelease(releaseEdits.Name);
            release.Name = releaseEdits.Name;
            release.ReleaseDate = DateTime.Parse(releaseEdits.ReleaseDate, CultureInfo.CurrentCulture);
            release.ReleaseManager = releaseEdits.ReleaseManager;
            Core.Repo.SaveRelease(release);

            IList<IVersionWork> versions = Core.Repo.GetVersionsInRelease(release.Name);

            foreach(ReleaseComponentViewModel component in releaseEdits.Components)
            {
                var version = versions.FirstOrDefault(v => v.Component.Name == component.Name);
                if (version != null && component.Included) // update
                {
                    version.StartRevision = long.Parse(component.StartRevision, CultureInfo.InvariantCulture);
                    version.EndRevision = ParseRevisionText(component.EndRevision);
                    version.SelectedRevision = ParseRevisionText(component.SelectedRevision);
                    Core.Repo.SaveVersion(version.VersionData());
                }
                else if (version != null && !component.Included) //delete
                {
                    Core.Repo.DeleteVersion(version.VersionData());
                }
                else if (version == null && component.Included)
                {
                    var newVersion = Core.Repo.CreateVersion(
                        release.Name,
                        component.Name,
                        long.Parse(component.StartRevision, CultureInfo.InvariantCulture),
                        ParseRevisionText(component.EndRevision));
                    Core.Repo.SaveVersion(newVersion);
                }
            }

            return RedirectToAction("Edit");
        }

        private long? ParseRevisionText(string value)
        {
            if (string.Equals("Head", value, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            return long.Parse(value, CultureInfo.InvariantCulture);
        }

        public ActionResult List()
        {
            IEnumerable<ReleaseViewModel> releases = Core.Repo.GetReleases().Select(r => new ReleaseViewModel(r));
            return View(releases);
        }

        public ActionResult Summary(string releaseName)
        {
            return this.ViewRelease("Summary", releaseName);
        }

        public ActionResult Tickets(string releaseName)
        {
            return this.ViewRelease("Tickets", releaseName);
        }

        private ActionResult ViewRelease(string view, string releaseName)
        {
            IRelease release = Core.Repo.GetRelease(releaseName);
            if (release == null)
            {
                return new ErrorController().Message("release not found");
            }
            IList<IVersionWork> versionsInRelease = Core.Repo.GetVersionsInRelease(releaseName);
            return View(view, new ReleaseViewModel(release, versionsInRelease));
        }

        public ActionResult Wip(string releaseName)
        {
            return this.ViewRelease("Wip", releaseName);
        }

        public ActionResult Delete(string releaseName)
        {
            IRelease release = Core.Repo.GetRelease(releaseName);
            Core.Repo.DeleteRelease(release);
            return this.RedirectToAction("List");
        }
    }
}