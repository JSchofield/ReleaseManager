namespace ReleaseManager.Web.Controllers
{
    using System.Web.Mvc;

    [HandleError]
    public class ErrorController : Controller
    {
        public ActionResult Message(string message)
        {
            return base.View();
        }
    }
}

