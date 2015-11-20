using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
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
        public ActionResult Index(string namefield, string emailfield, string sex, string companyfield, HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength <= 0) return Json(new { IsSuccess = false, Message = "Unable to Upload File" });
            try
            {
                var files = new List<string>();
                var appType = ApplicationType.Application1;
                var userId = Guid.NewGuid();
                var path = Server.MapPath(DirectoryHelper.GetApplicationAttachmentDirPath(ApplicationType.Application1));

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //string path = Path.Combine(Server.MapPath("~/test"),
                //                           Path.GetFileName(file.FileName));


                var fileName = GetFilenameWithTimeStamp(file.FileName);
                var fullPath = Path.Combine(path, fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    throw new Exception($"File {fullPath} already exists. File not saved.");
                }
                files.Add(fullPath);

                file.SaveAs(fullPath);

                var zipFile = DirectoryHelper.GetZipFilePath(appType, userId);
                ZipHelper.CreateZipFromFiles(files, appType, namefield, userId);
                var application1Dto = new Application1Dto
                {
                    UserId = userId.ToString(),
                    Name = namefield ?? "",
                    Email = emailfield ?? "",
                    ZipFilePath = zipFile,
                    Sex = sex ?? "",
                    Company = companyfield ?? ""
                };

                JsonHelper.Save(application1Dto, appType);
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
                       $" Attachment Link: {application1Dto.ZipFilePath}";
            var subject = "File Send";
            Emailer.SendMail(from, to, from, subject, body);
        }



    }
}