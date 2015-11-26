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
        public ActionResult SubmitForm1(ViewModelForm1Data formData)
        {

            if (ValidateFileInput(formData))
            {
                TempData["Status"] = "Error";
                return View("InputWizard");
            }
            try
            {
                var appType = ApplicationType.Application1;
                var appId = Guid.NewGuid();
                var files = new List<string>
                    {
                    SaveUploadedFiles(formData.file1),
                    SaveUploadedFiles(formData.file2)
                };

                var zipFilePath = DirectoryHelper.GetZipFilePath(appType, appId, formData.Name);
                var zipFilePhysicalPath = Server.MapPath(zipFilePath);

                var application1Dto = new Application1Dto
                {
                    AppId = appId.ToString(),
                    Name = formData.Name ?? "",
                    Email = formData.Email ?? "",
                    Sex = formData.Sex ?? "",
                    Company = formData.Company ?? "",
                    ZipFilePath = ".." + zipFilePath,
                    CreateDate = DateTime.Now
                };

                var userDataFilePath = CreateUserDataFile(application1Dto);
                files.Add(userDataFilePath);

                ZipHelper.CreateZipFromFiles(files, zipFilePhysicalPath);

                var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(appType);
                JsonHelper.Save(application1Dto, Server.MapPath(dataFilePath));
                SendEmailToPredefinedAdressee(application1Dto);
                return View("Success");
            }
            catch (Exception ex)
            {
                TempData["Status"] = "Error";
                return View("InputWizard");
            }
        }

        private bool ValidateFileInput(ViewModelForm1Data formData)
        {
            var maxsize = 1024 * 1024 * 100;
            return formData.file1 == null || (formData.file1.ContentLength <= 0 || formData.file1.ContentLength > maxsize) || 
                formData.file2 == null || (formData.file2.ContentLength <= 0 || formData.file2.ContentLength > maxsize);
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
            var to = Settings.Default.ToEmailAddress;
            var body = $"User Name: {application1Dto.Name}<br/>" +
                       $"Email: {application1Dto.Email}<br/>" +
                       $"Sex: {application1Dto.Sex}<br/>" +
                       $" Attachment Link: {GetDownloadLinkForFile(application1Dto.AppId)}";
            var subject = "File Send";
            Emailer.SendMail(to, subject, body);
        }

        private string CreateUserDataFile(Application1Dto application1Dto)
        {

            var fileName = GetFilenameWithTimeStamp(application1Dto.Name + "_data.pdf");
            var path = Server.MapPath(DirectoryHelper.GetApplicationAttachmentDirPath(ApplicationType.Application1));
            var fullPath = Path.Combine(path, fileName);
            var downloadLink = GetDownloadLinkForFile(application1Dto.AppId);
            if (!System.IO.File.Exists(fullPath))
            {

                // Create a file to write to.
                PdfUtility.CreatePdf(application1Dto, fullPath, downloadLink);
            }

            return fullPath;
        }

        private string GetDownloadLinkForFile(string appId)
        {
            var fileLink = "Admin/DownloadZipFile?appId=" + appId;

            var rootUri = Request.UrlReferrer?.AbsoluteUri ?? "";

            return rootUri + fileLink;
        }

        #endregion
    }
}