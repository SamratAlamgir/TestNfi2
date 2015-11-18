using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;
using NFI.Utility;

namespace NFI.Controllers
{
    public class HomeController : Controller
    {
        private const string TimestampPattern = "yyyyMMddHHmmssfff";
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult InputWizard()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SumitUserInfoWithFile(string name, string email, string sex, string company, string data, string fileName)
        {
            try
            {
                var appType = ApplicationType.Application1;
                var fileContentNotManipulated = data;
                var content = fileContentNotManipulated.Substring(fileContentNotManipulated.IndexOf("base64", StringComparison.Ordinal) + 7);
                var fileContent = Convert.FromBase64String(content);
                var path = DirectoryHelper.GetApplicationAttachmentDirPath(ApplicationType.Application1);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fileName = GetFilenameWithTimeStamp(fileName);
                var fullPath = Path.Combine(path, fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    throw new Exception($"File {fullPath} already exists. File not saved.");
                }

                System.IO.File.WriteAllBytes(fullPath, fileContent);
                var application1Dto = new Application1Dto
                {
                    Name = name ?? "",
                    Email = email ?? "",
                    FilePath = fullPath,
                    Sex = sex ?? "",
                };
                JsonHelper.Save(application1Dto, appType);
                return Json(new { IsSuccess = true, Message = "File uploaded successfully" });
            }
            catch (Exception exception)
            {
                return Json(new { IsSuccess = false, Message = "Unable to Upload File" });
            }

        }
        private string GetFilenameWithTimeStamp(string filename)
        {
            var extension = Path.GetExtension(filename);
            var timeStamp = DateTime.Now.ToString(TimestampPattern);
            return string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(filename), timeStamp, extension);
        }
    }
}