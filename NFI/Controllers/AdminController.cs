using System.IO;
using System.Linq;
using System.Web.Mvc;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;
using System.Collections.Generic;

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
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(appType);

            dynamic result = null;

            if (appType == ApplicationType.Insentivordning)
            {
                result = GetInsentivordningDtoList(dataFilePath, includeArchive);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private List<InsentivordningDto> GetInsentivordningDtoList(string dataFilePath, bool includeArchive)
        {
            var result = JsonHelper.GetCollections<InsentivordningDto>(Server.MapPath(dataFilePath)).ToList();

            if (!includeArchive)
            {
                result = result.Where(x => !x.IsArchived).ToList();
            }

            return result;
        }

        public bool MarkAsArchive(string appId)
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(ApplicationType.Sorfond);
            var resultSet = JsonHelper.GetCollections<Application1Dto>(Server.MapPath(dataFilePath));

            var selectedApp = resultSet.Single(x => x.AppId == appId);
            selectedApp.IsArchived = true;

            
            JsonHelper.Save(resultSet, Server.MapPath(dataFilePath));

            return true;
        }

        public ActionResult ShowDetail(string appId, ApplicationType appType)
        {
            var viewName = "";
            object selectedApp = null;

            if (appType == ApplicationType.Insentivordning)
            {
                viewName = "InsentivordningDetail";
                selectedApp = GetApplicationDto<InsentivordningDto>(appId, appType);
            }
            else if (appType == ApplicationType.Sorfond)
            {
                viewName = "SorfondDetail";
                selectedApp = GetApplicationDto<SorfondDto>(appId, appType);
            }

            return View(viewName, selectedApp);
        }

        public FileResult DownloadZipFile(string appId, ApplicationType appType)
        {
            BaseAppDto selectedApp = null;

            if (appType == ApplicationType.Insentivordning)
            {
                selectedApp = GetApplicationDto<InsentivordningDto>(appId, appType);
            }
            else if (appType == ApplicationType.Sorfond)
            {
                selectedApp = GetApplicationDto<SorfondDto>(appId, appType);
            }

            var filePath = Server.MapPath(selectedApp?.ZipFilePath);

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName =  Path.GetFileName(filePath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        private T GetApplicationDto<T> (string appId, ApplicationType appType)
            where T : BaseAppDto
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(appType);
            var resultSet = JsonHelper.GetCollections<T>(Server.MapPath(dataFilePath));

            return resultSet.Single(x => x.AppId.ToString() == appId);
        }
    }
}
