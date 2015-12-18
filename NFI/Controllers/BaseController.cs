using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;

namespace NFI.Controllers
{
    public class BaseController : Controller
    {
        private const string TimestampPattern = "yyyyMMddHHmm";
        protected List<string> FilePathList = new List<string>();

        public string CreateUserDataFile<T>(T appDto, ApplicationType appType, string fileNamePart)
        {
            var viewName = DetailViewNames.ViewName(appType);
            var fileName = GetUpdatedFileName("Application data.pdf", fileNamePart);
            var path = DirectoryHelper.GetApplicationAttachmentDirPath(appType);
            var fullPath = Path.Combine(path, fileName);
            var htmlString = GetApplicationDetailsStringHtml(this, viewName, appDto);
            var startIndex = htmlString.IndexOf("<head>", StringComparison.Ordinal) + 6;
            var length = htmlString.IndexOf("</head>", StringComparison.Ordinal) - startIndex;
            var replace = htmlString.Substring(startIndex, length);
            htmlString = htmlString.Replace(replace, "");
            PdfUtility.SavePdfFile(htmlString, fullPath, Server.MapPath("~"));
            return fullPath;
        }

        protected string GetDownloadLinkForFile(string appId, ApplicationType appType)
        {
            var fileLink = "Admin/DownloadZipFile/" + (int)appType + "/" + appId;
            return new Uri(GetBaseUri(), fileLink).ToString();
        }

        protected string GetDetailViewLink(string appId, ApplicationType appType)
        {
            var fileLink = "Admin/ShowDetail/" + (int)appType + "/" + appId;
            return new Uri(GetBaseUri(), fileLink).ToString();
        }

        private Uri GetBaseUri()
        {
            return
                new Uri(Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/");
        }


        protected string GetApplicationDetailsStringHtml(Controller controller, string viewName, object model)
        {

            TrimPathAndOnlyFileName(model);
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindView(controller.ControllerContext, viewName, null);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.ToString();
            }
        }


        protected void TrimPathAndOnlyFileName(Object obj)
        {
            if (obj == null) return;

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

                    if(filePaths == null) continue;

                    filePaths = filePaths.Where(f => !string.IsNullOrEmpty(f)).ToList();
                    var fileNames = filePaths.Select(Path.GetFileName).ToList();
                    propertyInfo.SetValue(obj, fileNames);
                }
                else if (propertyInfo.Name.Contains("Path"))
                {
                    var filePath = (string)propertyInfo.GetValue(obj);

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        propertyInfo.SetValue(obj, Path.GetFileName(filePath));
                    }
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

        protected void SaveApplication<T>(T application, ApplicationType applicationType, string userName, string fileNamePart) where T : BaseAppDto
        {
            LogWriter.Write("Save called for " + applicationType);

            application.AppId = Guid.NewGuid();
            application.CreateTime = DateTime.Now;

            FilePathList = new List<string>();
            SaveFilesAndSetFilePath(application, applicationType, fileNamePart);
            FilePathList = FilePathList.Where(f => !string.IsNullOrEmpty(f)).ToList();

            FilePathList.Add(CreateUserDataFile(application, applicationType, fileNamePart)); // User data file

            var zipFilePath = DirectoryHelper.GetZipFilePath(applicationType, application.AppId, userName);
            application.ZipFilePath = zipFilePath;

            var zipFilePhysicalPath = zipFilePath;
            ZipHelper.CreateZipFromFiles(FilePathList, zipFilePhysicalPath);

            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(applicationType);
            JsonHelper.Save<T>(application, dataFilePath);
        }

        #region File Helper Methods


        private void SaveFilesAndSetFilePath(Object obj, ApplicationType applicationType, string fileNamePart)
        {
            var type = obj.GetType();
            var fieldInfos =
                type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(f => NotPrimitive(f.PropertyType))
                    .ToList();
            var allFieldPaths = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(f => f.Name.Contains("Path"))
                    .ToList();
            foreach (var fieldInfo in fieldInfos)
            {
                if (fieldInfo.PropertyType == typeof(HttpPostedFileBase))
                {
                    var filePath = SaveUploadedFile((HttpPostedFileBase)fieldInfo.GetValue(obj),
                        applicationType, fileNamePart);
                    FilePathList.Add(filePath);
                    var fieldPath = allFieldPaths.FirstOrDefault(c => c.Name == fieldInfo.Name + "Path");
                    fieldPath?.SetValue(obj, filePath);
                }
                else if (fieldInfo.PropertyType.IsGenericType
                         && fieldInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
                         && fieldInfo.PropertyType.GetGenericArguments()[0] == typeof(HttpPostedFileBase))
                {
                    var files = (List<HttpPostedFileBase>)fieldInfo.GetValue(obj);
                    if (files != null)
                    {
                        var filePaths =
                            files.Select(
                                httpPostedFileBase => SaveUploadedFile(httpPostedFileBase, applicationType, fileNamePart))
                                .ToList();
                        FilePathList.AddRange(filePaths);
                        var fieldPath = allFieldPaths.FirstOrDefault(c => c.Name == fieldInfo.Name + "Paths");
                        fieldPath?.SetValue(obj, filePaths);
                    }
                }
                else if (fieldInfo.PropertyType.IsGenericType &&
                         fieldInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
                         && fieldInfo.PropertyType.GetGenericArguments()[0].GetInterfaces().Contains(typeof(IMember)))
                {

                    var objs = fieldInfo.GetValue(obj) as IList;
                    if (objs != null)
                    {
                        foreach (var member in objs)
                        {
                            SaveFilesAndSetFilePath(member, applicationType, fileNamePart);
                        }
                    }
                }

                else if (fieldInfo.PropertyType.GetInterfaces().Contains(typeof(IMember)))
                {
                    var o = fieldInfo.GetValue(obj);
                    if (o != null)
                        SaveFilesAndSetFilePath(o, applicationType, fileNamePart);
                }
            }
        }

        private static bool NotPrimitive(Type type)
        {
            return !(type.IsPrimitive || type == typeof(Guid)
                     || type == typeof(string));
        }
        private string GetUpdatedFileName(string fileName, string partToAppend)
        {
            var extension = Path.GetExtension(fileName);
            var g = Guid.NewGuid().ToString().Split('-')[0];
            return $"{Path.GetFileNameWithoutExtension(fileName)}_{partToAppend}_{g}{extension}";
        }

        private string SaveUploadedFile(HttpPostedFileBase file, ApplicationType appType, string fileNamePart)
        {
            if (file == null)
                return "";

            var networkPath = DirectoryHelper.GetApplicationAttachmentDirPath(appType);
            var physicalPath = networkPath;
            if (!Directory.Exists(physicalPath))
            {
                Directory.CreateDirectory(physicalPath);
            }
            var fileName = GetUpdatedFileName(file.FileName, fileNamePart);
            var fullPath = Path.Combine(physicalPath, fileName);
            if (System.IO.File.Exists(fullPath))
            {
                throw new Exception($"File {fullPath} already exists. File not saved.");
            }
            file.SaveAs(fullPath);
            return fullPath;
        }

        #endregion

    }
}