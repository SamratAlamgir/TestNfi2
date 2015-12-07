using System;
using System.Web.Mvc;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;
using NFI.Properties;

namespace NFI.Controllers
{
    public class InsentivordningController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Save(InsentivordningDto appDto)
        {
            try
            {
                var appType = ApplicationType.Insentivordning;
                SaveApplication(appDto, appType, appDto.ProduksjonsforetaketsNavn);

                //TODO: Send the mails
                var mailSubject = "INSENTIVORDNING " + appDto.TittelpåProsjektet;
                var mailBody = "Hi,<br/>A new application has been submitted.<br/><br/>" +
                    "Application Details: <a href = '" + GetDetailViewLink(appDto.AppId.ToString(), appType) + "'> Click Here </a>" +
                    "<br/>" +
                    "Download Zip File: <a href='" + GetDownloadLinkForFile(appDto.AppId.ToString(), appType) + "'> Click Here </a> <br/>";

                var responseText = GetApplicationDetailsStringHtml(this, "../Admin/InsentivordningDetail", appDto);

                mailBody += responseText;

                var mailTo = Settings.Default.ToEmailAddress;
                CommunicationHelper.SendEmailToArchivist(mailSubject, mailBody, mailTo, FilePathList);
                return View("Success");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex;
                return View("Error");
            }
        }
    }
}