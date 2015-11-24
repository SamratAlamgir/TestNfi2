using System.Linq;
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

        public JsonResult GetApplications(ApplicationType appType, bool includeArchive)
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(ApplicationType.Application1);
            var result = JsonHelper.GetCollections<Application1Dto>(Server.MapPath(dataFilePath))
                .OrderBy(x => x.IsArchived).ThenByDescending(x => x.CreateDate).ToList();

            if (!includeArchive)
            {
                result = result.Where(x => !x.IsArchived).ToList();
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public bool MarkAsArchive(string appId)
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(ApplicationType.Application1);
            var resultSet = JsonHelper.GetCollections<Application1Dto>(Server.MapPath(dataFilePath));

            var selectedAppp = resultSet.Single(x => x.AppId == appId);
            selectedAppp.IsArchived = true;

            
            JsonHelper.Save(resultSet, Server.MapPath(dataFilePath));

            return true;
        }
    }
}
