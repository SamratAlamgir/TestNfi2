using System.Web.Mvc;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;

namespace NFI.Controllers
{
    [BasicAuthenticationAttribute("admin", "test123", BasicRealm = "admin")]
    public class AdminController : Controller
    {
       
        public ActionResult Index()
        {
            return View("ApplicationList");
        }

        public ActionResult ApplicationList()
        {
            return View("ApplicationList");
        }

        public JsonResult GetApplications()
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(ApplicationType.Application1);
            var result = JsonHelper.GetCollections<Application1Dto>(Server.MapPath(dataFilePath));

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
