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
        public ActionResult SubmitForm1(ViewModelFrom1Data formData)
        {
            if (formData.file1 == null || formData.file1.ContentLength <= 0 || formData.file2 == null || formData.file2.ContentLength <= 0) return Json(new { IsSuccess = false, Message = "Unable to Upload File" });
            try
            {
                var appType = ApplicationType.Application1;
                var userId = Guid.NewGuid();
                var files = new List<string>
                {
                    SaveUploadedFiles(formData.file1),
                    SaveUploadedFiles(formData.file2)
                };

                var zipFilePath = DirectoryHelper.GetZipFilePath(appType, userId, formData.Name);
                var zipFilePhysicalPath = Server.MapPath(zipFilePath);

                var application1Dto = new Application1Dto
                {
                    UserId = userId.ToString(),
                    Name = formData.Name ?? "",
                    Email = formData.Email ?? "",
                    Sex = formData.Sex ?? "",
                    Company = formData.Company ?? "",
                    ZipFilePath = ".." + zipFilePath
                };

                var userDataFilePath = CreateUserDataFile(application1Dto);
                files.Add(userDataFilePath);

                ZipHelper.CreateZipFromFiles(files, zipFilePhysicalPath);

                var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(appType);
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

        #region Helper Mehtods

        private string GetFilenameWithTimeStamp(string file1Name)
        {
            var extension = Path.GetExtension(file1Name);
            var timeStamp = DateTime.Now.ToString(TimestampPattern);
            return $"{Path.GetFileNameWithoutExtension(file1Name)}_{timeStamp}{extension}";
        }

        private string SaveUploadedFiles(HttpPostedFileBase file)
        {
            var networkPath = DirectoryHelper.GetApplicationAttachmentDirPath(ApplicationType.Application1);
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

        #endregion
    }
}