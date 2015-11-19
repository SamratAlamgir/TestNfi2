using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult InputWizard()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SumitUserInfoWithFile(string name, string email, string sex, string company, FileDto[] fileDtos)
        {
            var files = new List<string>();
            try
            {
                var appType = ApplicationType.Application1;
                var userId = Guid.NewGuid();
                foreach (var fileDto in fileDtos)
                {
                    var fileContentNotManipulated = fileDto.Content;
                    var content = fileContentNotManipulated.Substring(fileContentNotManipulated.IndexOf("base64", StringComparison.Ordinal) + 7);
                    var fileContent = Convert.FromBase64String(content);
                    var path = DirectoryHelper.GetApplicationAttachmentDirPath(ApplicationType.Application1);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    fileDto.Name = GetFilenameWithTimeStamp(fileDto.Name);
                    var fullPath = Path.Combine(path, fileDto.Name);
                    if (System.IO.File.Exists(fullPath))
                    {
                        throw new Exception($"File {fullPath} already exists. File not saved.");
                    }
                    files.Add(fullPath);
                    System.IO.File.WriteAllBytes(fullPath, fileContent);

                }
                var zipFile = DirectoryHelper.GetZipFilePath(appType, userId);
                ZipHelper.CreateZipFromFiles(files, appType, name, userId);
                var application1Dto = new Application1Dto
                {
                    UserId = userId.ToString(),
                    Name = name ?? "",
                    Email = email ?? "",
                    ZipFilePath = zipFile,
                    Sex = sex ?? "",
                };

                JsonHelper.Save(application1Dto, appType);
                SendEmailToPredefinedAdressee(application1Dto);
                return Json(new { IsSuccess = true, Message = "File uploaded successfully" });
            }
            catch (Exception exception)
            {
                return Json(new { IsSuccess = false, Message = "Unable to Upload File" });
            }

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

        private string GetFilenameWithTimeStamp(string filename)
        {
            var extension = Path.GetExtension(filename);
            var timeStamp = DateTime.Now.ToString(TimestampPattern);
            return $"{Path.GetFileNameWithoutExtension(filename)}_{timeStamp}{extension}";
        }
    }
}