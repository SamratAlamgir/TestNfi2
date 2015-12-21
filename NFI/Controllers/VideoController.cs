using System;
using System.Web.Mvc;
using NFI.App_Start;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;
using NFI.Properties;

namespace NFI.Controllers
{
    public class VideoController : BaseController
    {
        [CaptchaAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(VideoDto appDto)
        {
            try
            {
                var appType = ApplicationType.Video;
                var mailSubject = "TILSKUDD TIL VIDEODISTRIBUSJON " + appDto.ProsjektetsTittel;

                SaveApplication(appDto, appType, appDto.NavnKontaktpersonDenneSøknaden, mailSubject);

                // Send mail to archivist

                var mailBody = MailTemplate.GetMailBodyForAdmin(appDto.AppId, appType);

                var responseText = GetApplicationDetailsStringHtml(this, DetailViewNames.ViewName(appType), appDto);

                mailBody += responseText;

                // Mail to admin
                var mailTo = Settings.Default.ToEmailAddress;
                CommunicationHelper.SendEmailToAdmin(mailSubject, mailBody, mailTo, appDto.Epostadressekontaktperson, appDto.NavnKontaktpersonDenneSøknaden, FilePathList);

                // Send mail to applicant
                mailSubject = "Tilskudd til videodistribusjon søknad sendt";
                mailBody = MailTemplate.GetMailBodyForApplicant(appType);

                CommunicationHelper.SendConfirmationEmailToUser(mailSubject, mailBody, appDto.Epostadressekontaktperson);

                return View("Success");
            }
            catch (Exception ex)
            {
                LogWriter.Write(ex.ToString(), "Error");
                ViewBag.error = ex;
                return View("Error");
            }

        }
    }
}