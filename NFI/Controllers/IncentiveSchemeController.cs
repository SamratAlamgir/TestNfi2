using System;
using System.Web.Mvc;
using NFI.App_Start;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;
using NFI.Properties;

namespace NFI.Controllers
{
    public class IncentiveSchemeController : BaseController
    {
        [CaptchaAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(IncentiveSchemeDto appDto)
        {
            try
            {
                var appType = ApplicationType.IncentiveScheme;
                var mailSubject = "INSENTIVORDNING " + appDto.ProjectTitle;

                SaveApplication(appDto, appType, appDto.NameProducer, mailSubject);

                // Send mail to archivist

                var mailBody = MailTemplate.GetMailBodyForAdmin(true);

                var responseText = GetApplicationDetailsStringHtml(this, DetailViewNames.ViewName(appType), appDto);

                mailBody += responseText;

                var mailTo = Settings.Default.ToEmailAddress;
                CommunicationHelper.SendEmailToAdmin(mailSubject, mailBody, mailTo, appDto.Email, appDto.NameApplicant, FilePathList);

                // Send mail to applicant
                mailSubject = "Insentivordning submitted successfully";
                mailBody = MailTemplate.GetMailBodyForApplicant(appType);

                CommunicationHelper.SendConfirmationEmailToUser(mailSubject, mailBody, appDto.Email);
                CommunicationHelper.SendConfirmationEmailToUser(mailSubject, mailBody, appDto.EmailContactInfo);

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