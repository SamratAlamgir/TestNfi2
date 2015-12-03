using System;
using System.IO;
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

        public string GetFilenameWithTimeStamp(string file1Name)
        {
            var extension = Path.GetExtension(file1Name);
            var timeStamp = DateTime.Now.ToString(TimestampPattern);
            return $"{Path.GetFileNameWithoutExtension(file1Name)}_{timeStamp}{extension}";
        }

        public string SaveUploadedFile(HttpPostedFileBase file, ApplicationType appType)
        {
            if (file == null)
                return null;

            var networkPath = DirectoryHelper.GetApplicationAttachmentDirPath(appType);
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

        public void SendEmailToPredefinedAdressee(Application1Dto application1Dto, ApplicationType appType)
        {
            var to = Settings.Default.ToEmailAddress;
            var body = $"User Name: {application1Dto.Name}<br/>" +
                       $"Email: {application1Dto.Email}<br/>" +
                       $"Sex: {application1Dto.Sex}<br/>" +
                       $"Attachment Link: {GetDownloadLinkForFile(application1Dto.AppId, appType)}";
            var subject = "File Send";
            Emailer.SendMail(to, subject, body);
        }

        public string CreateUserDataFile<T>(T appDto, ApplicationType appType)
        {
            var type = appDto.GetType();
            var appId = type.GetProperty("AppId").GetValue(appDto);

            var fileName = GetFilenameWithTimeStamp("user_data.pdf");
            var path = Server.MapPath(DirectoryHelper.GetApplicationAttachmentDirPath(appType));
            var fullPath = Path.Combine(path, fileName);
            var downloadLink = GetDownloadLinkForFile(appId.ToString(), appType);
            if (!System.IO.File.Exists(fullPath))
            {
                // Create a file to write to.
                PdfUtility.CreatePdf(appDto, fullPath, downloadLink);
            }

            return fullPath;
        }

        public string CreateTextFile<T>(T appDto, ApplicationType appType)
        {
            var type = appDto.GetType();
            var appId = type.GetProperty("AppId").GetValue(appDto);

            var fileName = GetFilenameWithTimeStamp("user_data.txt");
            var path = Server.MapPath(DirectoryHelper.GetApplicationAttachmentDirPath(appType));
            var fullPath = Path.Combine(path, fileName);
            var downloadLink = GetDownloadLinkForFile(appId.ToString(), appType);
            if (!System.IO.File.Exists(fullPath))
            {
                // Create a file to write to.
                using (StreamWriter sw = System.IO.File.CreateText(fullPath))
                {
                    sw.WriteLine(appDto.ToString());
                }
            }

            return fullPath;
        }

        public string GetDownloadLinkForFile(string appId, ApplicationType appType)
        {
            var fileLink = "Admin/DownloadZipFile?appId=" + appId + "&appType=" + (int)appType;
            return new Uri(GetBaseUri(), fileLink).ToString(); 
        }

        public string GetDetailViewLink(string appId, ApplicationType appType)
        {
            var fileLink = "Admin/DownloadZipFile?appId=" + appId + "&appType=" + (int)appType;
            return new Uri(GetBaseUri(), fileLink).ToString();
        }

        private Uri GetBaseUri()
        {
            return new Uri(Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/");
        }
    }
}