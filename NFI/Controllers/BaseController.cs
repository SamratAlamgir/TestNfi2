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
using NFI.Properties;
using NFI.Utility;

namespace NFI.Controllers
{
    public class BaseController : Controller
    {
        private const string TimestampPattern = "yyyyMMddHHmmssfff";
        protected List<string> FilePathList = new List<string>();
        public string GetFilenameWithTimeStamp(string file1Name)
        {
            var extension = Path.GetExtension(file1Name);
            var timeStamp = DateTime.Now.ToString(TimestampPattern);
            return $"{Path.GetFileNameWithoutExtension(file1Name)}_{timeStamp}{extension}";
        }

        public string SaveUploadedFile(HttpPostedFileBase file)
        {
            if (file == null)
                return null;

            var networkPath = DirectoryHelper.GetApplicationAttachmentDirPath(ApplicationType.Sorfond);
            var physicalPath = Server.MapPath(networkPath);
            if (!Directory.Exists(physicalPath))
            {
                Directory.CreateDirectory(physicalPath);
            }
            var file1Name = GetFilenameWithTimeStamp(file.FileName);
            var fullPath = Path.Combine(physicalPath, file1Name);
            if (System.IO.File.Exists(fullPath))
            {
                throw new Exception($"File {fullPath} already exists. File not saved.");
            }
            file.SaveAs(fullPath);
            return fullPath;
        }

        public void SendEmailToPredefinedAdressee(Application1Dto application1Dto)
        {
            var to = Settings.Default.ToEmailAddress;
            var body = $"User Name: {application1Dto.Name}<br/>" +
                       $"Email: {application1Dto.Email}<br/>" +
                       $"Sex: {application1Dto.Sex}<br/>" +
                       $"Attachment Link: {GetDownloadLinkForFile(application1Dto.AppId)}";
            var subject = "File Send";
            Emailer.SendMail(to, subject, body);
        }

        public string CreateUserDataFile<T>(T appDto)
        {
            var type = appDto.GetType();
            var appId = type.GetProperty("AppId").GetValue(appDto);

            var fileName = GetFilenameWithTimeStamp("user_data.pdf");
            var path = Server.MapPath(DirectoryHelper.GetApplicationAttachmentDirPath(ApplicationType.Sorfond));
            var fullPath = Path.Combine(path, fileName);
            var downloadLink = GetDownloadLinkForFile(appId.ToString());
            if (!System.IO.File.Exists(fullPath))
            {
                // Create a file to write to.
                PdfUtility.CreatePdf(appDto, fullPath, downloadLink);
            }

            return fullPath;
        }

        public string GetDownloadLinkForFile(string appId)
        {
            var fileLink = "Admin/DownloadZipFile?appId=" + appId;
            return new Uri(GetBaseUri(), fileLink).ToString(); 
        }

        public string GetDetailViewLink(string appId)
        {
            var fileLink = "Admin/ShowDetail?appId=" + appId;
            return new Uri(GetBaseUri(), fileLink).ToString();
        }

        private Uri GetBaseUri()
        {
            return new Uri(Request.UrlReferrer?.AbsoluteUri ?? "");
        }

        protected void SaveFilesAndSetFilePath(Object obj)
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
                    var filePath = SaveUploadedFile((HttpPostedFileBase)fieldInfo.GetValue(obj));
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
                        var filePaths = files.Select(SaveUploadedFile).ToList();
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
                            SaveFilesAndSetFilePath(member);
                        }
                    }
                }

                else if (fieldInfo.PropertyType.GetInterfaces().Contains(typeof(IMember)))
                {
                    var o = fieldInfo.GetValue(obj);
                    if (o != null)
                        SaveFilesAndSetFilePath(o);
                }

            }
        }

        private static bool NotPrimitive(Type type)
        {
            return !(type.IsPrimitive || type == typeof(Guid)
                     || type == typeof(string));
        }
    }
}