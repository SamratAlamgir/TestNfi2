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
        public ActionResult ShowDetail(ApplicationType appType, string appId)
        {
            var viewName = "";
            object selectedApp = null;
            try
            {
                switch (appType)
                {
                    case ApplicationType.Insentivordning:
                        viewName = "InsentivordningDetail";
                        selectedApp = GetApplicationDto<InsentivordningDto>(appId, appType);
                        break;
                    case ApplicationType.Sorfond:
                        viewName = "Sorfond/Details";
                        selectedApp = GetApplicationDto<SorfondDto>(appId, appType);
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
            }
            var filePath = System.Web.HttpContext.Current.Server.MapPath(selectedApp?.ZipFilePath);
            filePath = filePath.Replace(@"\Admin\DownloadZipFile", "");
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var fileName = Path.GetFileName(filePath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        private T GetApplicationDto<T>(string appId, ApplicationType appType)
            where T : BaseAppDto
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(appType);
            var resultSet = JsonHelper.GetCollections<T>(Server.MapPath(dataFilePath));

            return resultSet.Single(x => x.AppId.ToString() == appId);
        }

        #region helper method
        private void TrimPathAndOnlyFileName(Object obj)
        {
            var type = obj.GetType();
            var fieldInfos =
                type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(f => NotPrimitive(f.PropertyType))
                    .ToList();
            var allFieldPaths = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(f => f.Name.Contains("Path"))
                    .ToList();
            foreach (var propertyInfo in allFieldPaths)
            {
                if (propertyInfo.Name.Contains("Paths"))
                {
                    var filePaths = (List<string>)propertyInfo.GetValue(obj);
                    var fileNames = filePaths.Select(Path.GetFileName).ToList();
                    propertyInfo.SetValue(obj, fileNames);
                }
                else if (propertyInfo.Name.Contains("Path"))
                {
                    var filePath = (string)propertyInfo.GetValue(obj);
                    propertyInfo.SetValue(obj, Path.GetFileName(filePath));
                }
            }
            foreach (var fieldInfo in fieldInfos)
            {
                if (fieldInfo.PropertyType.IsGenericType &&
                           fieldInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
                           && fieldInfo.PropertyType.GetGenericArguments()[0].GetInterfaces().Contains(typeof(IMember)))
                {

                    var objs = fieldInfo.GetValue(obj) as IList;
                    if (objs != null)
                    {
                        foreach (var member in objs)
                        {
                            TrimPathAndOnlyFileName(member);
                        }
                    }
                }

                else if (fieldInfo.PropertyType.GetInterfaces().Contains(typeof(IMember)))
                {
                    var o = fieldInfo.GetValue(obj);
                    if (o != null)
                        TrimPathAndOnlyFileName(o);
                }

            }
        }
        private static bool NotPrimitive(Type type)
        {
            return !(type.IsPrimitive || type == typeof(Guid)
                     || type == typeof(string));
        }
        #endregion
    }
}
