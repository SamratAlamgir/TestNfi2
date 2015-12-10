using System;
using System.Web.Mvc;
using NFI.App_Start;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;
using NFI.Properties;

namespace NFI.Controllers
{
    public class UdsReisestotteController : BaseController
    {
        [CaptchaAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(UdsReisestotteDto appDto)
        {
            try
            {
                var appType = ApplicationType.UdsReisestotte;
                SaveApplication(appDto, appType, appDto.Søkersnavn);

                // Send mail to archivist
                var mailSubject = "UDs REISESTØTTE " + appDto.Målforreisen+ appDto.Søkersnavn;
                var mailBody = "Hi,<br/>A new application has been submitted.<br/><br/>" +
                    "Application Details: <a href = '" + GetDetailViewLink(appDto.AppId.ToString(), appType) + "'> Click Here </a>" +
                    "<br/>" +
                    "Download Zip File: <a href='" + GetDownloadLinkForFile(appDto.AppId.ToString(), appType) + "'> Click Here </a> <br/>";

                var responseText = GetApplicationDetailsStringHtml(this, "../Admin/UdsReisestotteDetail", appDto);

                mailBody += responseText;

                var mailTo = Settings.Default.ToEmailAddress;
                CommunicationHelper.SendEmail(mailSubject, mailBody, mailTo, FilePathList);

                // Send mail to applicant
                mailSubject = "UDs Reisestøtte søknad sendtt";
                mailBody = MailTemplate.GetMailBodyForApplicant(ApplicationType.UdsReisestotte);

                CommunicationHelper.SendEmail(mailSubject, mailBody, appDto.Søkersepost);

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