﻿using System.IO;
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
            var result = JsonHelper.GetCollections<Application1Dto>(Server.MapPath(dataFilePath)).ToList();

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

            var selectedApp = resultSet.Single(x => x.AppId == appId);
            selectedApp.IsArchived = true;

            
            JsonHelper.Save(resultSet, Server.MapPath(dataFilePath));

            return true;
        }

        public ActionResult ShowDetail(string appId)
        {
            var selectedApp = GetApplicationDto(appId);
            return View("Application1Detail", selectedApp);
        }

        public FileResult DownloadZipFile(string appId)
        {
            var selectedApp = GetApplicationDto(appId);

            var filePath = Server.MapPath(selectedApp.ZipFilePath);

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName =  Path.GetFileName(filePath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        private Application1Dto GetApplicationDto(string appId)
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(ApplicationType.Application1);
            var resultSet = JsonHelper.GetCollections<Application1Dto>(Server.MapPath(dataFilePath));

            return resultSet.Single(x => x.AppId == appId);
        }
    }
}
