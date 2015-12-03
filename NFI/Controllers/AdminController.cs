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
            string fileName = Path.GetFileName(filePath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        private Application1Dto GetApplicationDto(string appId)
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(ApplicationType.Sorfond);
            var resultSet = JsonHelper.GetCollections<Application1Dto>(Server.MapPath(dataFilePath));

            return resultSet.Single(x => x.AppId == appId);
        }
        public ActionResult SorfondDetails(string id)
        {
            try
            {
                var sorfond = GetSrofondDto(id);
                TrimPathAndOnlyFileName(sorfond);
                return View("Sorfond/Details", sorfond);

            }
            catch (Exception ex)
            {

                return View("Error");
            }

        }

        #region Sorfond

        private SorfondDto GetSrofondDto(string appId)
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(ApplicationType.Sorfond);
            var resultSet = JsonHelper.GetCollections<SorfondDto>(Server.MapPath(dataFilePath));

            return resultSet.Single(x => x.AppId.ToString() == appId);
        }

        #endregion

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
