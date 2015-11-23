using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using NFI.App_Start;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;
using NFI.Properties;
using NFI.Utility;

namespace NFI.Controllers
{
    [CaptchaAuthorize]
    public class HomeController : Controller
    {
        private const string TimestampPattern = "yyyyMMddHHmmssfff";

        [HttpPost]
        public ActionResult SubmitForm1(ViewModelFrom1Data formData)
        {
            if (ValidateFileInput(formData))
                return Json(new { IsSuccess = false, Message = "Unable to Upload File" });
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

                ZipHelper.CreateZipFromFiles(files, zipFilePhysicalPath);

                var application1Dto = new Application1Dto
                {
                    UserId = userId.ToString(),
                    Name = formData.Name ?? "",
                    Email = formData.Email ?? "",
                    Sex = formData.Sex ?? "",
                    Company = formData.Company ?? "",
                    ZipFilePath = ".." + zipFilePath
                };

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

        private bool ValidateFileInput(ViewModelFrom1Data formData)
        {
            var maxsize = 1024 * 1024 * 100;
            return formData.file1 == null || (formData.file1.ContentLength <= 0 || formData.file1.ContentLength > maxsize) || formData.file2 == null || (formData.file2.ContentLength <= 0 || formData.file2.ContentLength > maxsize);
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
                       $" Attachment Link: {application1Dto.ZipFilePath}";
            var subject = "File Send";
            Emailer.SendMail(from, to, from, subject, body);
        }

        #endregion
    }
}