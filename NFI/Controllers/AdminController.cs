using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;
using System.Collections.Generic;
using System.Reflection;

namespace NFI.Controllers
{
    [BasicAuthenticationAttribute("admin", "test123", BasicRealm = "admin")]
    public class AdminController : BaseController
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
            var result = JsonHelper.GetCollections<InsentivordningDto>(dataFilePath).ToList();

            if (!includeArchive)
            {
                result = result.Where(x => !x.IsArchived).ToList();
            }

            return result;
        }

        public bool MarkAsArchive(string appId)
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(ApplicationType.Sorfond);
            var resultSet = JsonHelper.GetCollections<Application1Dto>(dataFilePath);

            var selectedApp = resultSet.Single(x => x.AppId == appId);
            selectedApp.IsArchived = true;


            JsonHelper.Save(resultSet, dataFilePath);

            return true;
        }
        public ActionResult ShowDetail(ApplicationType appType, string appId)
        {
            var viewName = "";
            object selectedApp = null;
            try
            {
                switch (appType)
                {
                    case ApplicationType.Sorfond:
                        viewName = "Sorfond/Details";
                        selectedApp = GetApplicationDto<SorfondDto>(appId, appType);
                        break;
                    case ApplicationType.Insentivordning:
                        viewName = "InsentivordningDetail";
                        selectedApp = GetApplicationDto<InsentivordningDto>(appId, appType);
                        break;
                    case ApplicationType.IncentiveScheme:
                        viewName = "IncentiveSchemeDetail";
                        selectedApp = GetApplicationDto<IncentiveSchemeDto>(appId, appType);
                        break;
                    case ApplicationType.Lansering:
                        viewName = "LanseringDetail";
                        selectedApp = GetApplicationDto<LanseringDto>(appId, appType);
                        break;
                }
                TrimPathAndOnlyFileName(selectedApp);
                return View(viewName, selectedApp);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public FileResult DownloadZipFile(ApplicationType appType, string appId)
        {
            BaseAppDto selectedApp = null;
            switch (appType)
            {
                case ApplicationType.Insentivordning:
                    selectedApp = GetApplicationDto<InsentivordningDto>(appId, appType);
                    break;
                case ApplicationType.Sorfond:
                    selectedApp = GetApplicationDto<SorfondDto>(appId, appType);
                    break;
                case ApplicationType.Lansering:
                    selectedApp = GetApplicationDto<LanseringDto>(appId, appType);
                    break;
            }
            var filePath = selectedApp?.ZipFilePath;
            filePath = filePath.Replace(@"\Admin\DownloadZipFile", "");
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var fileName = Path.GetFileName(filePath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        private T GetApplicationDto<T>(string appId, ApplicationType appType)
            where T : BaseAppDto
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(appType);
            var resultSet = JsonHelper.GetCollections<T>(dataFilePath);

            return resultSet.Single(x => x.AppId.ToString() == appId);
        }

    }
}
