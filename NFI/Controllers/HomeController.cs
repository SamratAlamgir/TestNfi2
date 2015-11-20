using System;
using System.Collections.Generic;
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
    public class HomeController : Controller
    {
        private const string TimestampPattern = "yyyyMMddHHmmssfff";

        [HttpPost]
        public ActionResult Index(string namefield, string emailfield, string sex, string companyfield, HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength <= 0) return Json(new { IsSuccess = false, Message = "Unable to Upload File" });
            try
            {
                var files = new List<string>();
                var appType = ApplicationType.Application1;
                var userId = Guid.NewGuid();

                var networkPath = DirectoryHelper.GetApplicationAttachmentDirPath(ApplicationType.Application1);
                var physicalPath = Server.MapPath(networkPath);

                if (!Directory.Exists(physicalPath))
                {
                    Directory.CreateDirectory(physicalPath);
                }

                var fileName = GetFilenameWithTimeStamp(file.FileName);
                var fullPath = Path.Combine(physicalPath, fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    throw new Exception($"File {fullPath} already exists. File not saved.");
                }
                files.Add(fullPath);

                file.SaveAs(fullPath);


                var zipFilePath = DirectoryHelper.GetZipFilePath(appType, userId, namefield);
                var zipFilePhysicalPath = Server.MapPath(zipFilePath);

                var application1Dto = new Application1Dto
                {
                    UserId = userId.ToString(),
                    Name = namefield ?? "",
                    Email = emailfield ?? "",
                    Sex = sex ?? "",
                    Company = companyfield ?? "",
                    ZipFilePath = ".." + zipFilePath
                };

                string userDataFilePath = CreateUserDataFile(application1Dto);
                files.Add(userDataFilePath);

                ZipHelper.CreateZipFromFiles(files, zipFilePhysicalPath);

                string dataFilePath = DirectoryHelper.GetApplicationDataFilePath(appType);
                JsonHelper.Save(application1Dto, Server.MapPath(dataFilePath));
                SendEmailToPredefinedAdressee(application1Dto);
                return Json(new { IsSuccess = true, Message = "File uploaded successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = "Unable to Upload File" });
            }
        }

        public ActionResult InputWizard()
        {
            return View();
        }
        private string GetFilenameWithTimeStamp(string filename)
        {
            var extension = Path.GetExtension(filename);
            var timeStamp = DateTime.Now.ToString(TimestampPattern);
            return $"{Path.GetFileNameWithoutExtension(filename)}_{timeStamp}{extension}";
        }
        private void SendEmailToPredefinedAdressee(Application1Dto application1Dto)
        {
            var from = Settings.Default.FromEmailAddress;
            var to = Settings.Default.ToEmailAddress;
            var body = $"User Name: {application1Dto.Name}<br/>" +
                       $"Email: {application1Dto.Email}<br/>" +
                       $"Sex: {application1Dto.Sex}<br/>" +
                       $" Attachment Link: {GetServerPathForFile(application1Dto.ZipFilePath)}";
            var subject = "File Send";
            Emailer.SendMail(from, to, from, subject, body);
        }

        private string CreateUserDataFile(Application1Dto application1Dto)
        {
            var fileContent = $"User Name: {application1Dto.Name} {Environment.NewLine}" +
                       $"Email: {application1Dto.Email} {Environment.NewLine}" +
                       $"Sex: {application1Dto.Sex} {Environment.NewLine}" +
                       $"Attachment Link: {GetServerPathForFile(application1Dto.ZipFilePath)}";

            string fileName = GetFilenameWithTimeStamp(application1Dto.Name + "_data.txt");
            string path = Server.MapPath(DirectoryHelper.GetApplicationAttachmentDirPath(ApplicationType.Application1));

            string fullPath = Path.Combine(path, fileName);
            
            if (!System.IO.File.Exists(fullPath))
            {
                // Create a file to write to.
                System.IO.File.WriteAllText(fullPath, fileContent);
            }

            return fullPath;
        }

        private string GetServerPathForFile(string filePath)
        {
             var rootUri = Request.UrlReferrer?.AbsoluteUri ?? "";

            return filePath.Replace("../", rootUri);
        }
    }
}