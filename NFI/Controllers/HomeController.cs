using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;

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
                var path = Server.MapPath (DirectoryHelper.GetApplicationAttachmentDirPath(ApplicationType.Application1));

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
        [HttpPost]
        //public ActionResult SumitUserInfoWithFile(string name, string email, string sex, string company, FileDto[] fileDtos)
        //{
        //    var files = new List<string>();
        //    try
        //    {
        //        var appType = ApplicationType.Application1;
        //        var userId = Guid.NewGuid();
        //        var path = DirectoryHelper.GetApplicationAttachmentDirPath(ApplicationType.Application1);

        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }

        //        foreach (var fileDto in fileDtos)
        //        {
        //            var fileContentNotManipulated = fileDto.Content;
        //            var content = fileContentNotManipulated.Substring(fileContentNotManipulated.IndexOf("base64", StringComparison.Ordinal) + 7);
        //            var fileContent = Convert.FromBase64String(content);

        //            fileDto.Name = GetFilenameWithTimeStamp(fileDto.Name);
        //            var fullPath = Path.Combine(path, fileDto.Name);
        //            if (System.IO.File.Exists(fullPath))
        //            {
        //                throw new Exception($"File {fullPath} already exists. File not saved.");
        //            }
        //            files.Add(fullPath);
        //            System.IO.File.WriteAllBytes(fullPath, fileContent);

        //        }
        //        var zipFile = DirectoryHelper.GetZipFilePath(appType, userId);
        //        ZipHelper.CreateZipFromFiles(files, appType, name, userId);
        //        var application1Dto = new Application1Dto
        //        {
        //            UserId = userId.ToString(),
        //            Name = name ?? "",
        //            Email = email ?? "",
        //            ZipFilePath = zipFile,
        //            Sex = sex ?? "",
        //        };

        //        JsonHelper.Save(application1Dto, appType);
        //        return Json(new { IsSuccess = true, Message = "File uploaded successfully" });
        //    }
        //    catch (Exception exception)
        //    {
        //        return Json(new { IsSuccess = false, Message = "Unable to Upload File" });
        //    }

        //}
        private string GetFilenameWithTimeStamp(string filename)
        {
            var extension = Path.GetExtension(filename);
            var timeStamp = DateTime.Now.ToString(TimestampPattern);
            return string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(filename), timeStamp, extension);
        }
    }
}