using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
                
                var mailBody = "Hi,<br/>A new application has been submitted.<br/><br/>" +
                    "Application Details: <a href = '" + GetDetailViewLink(appDto.AppId.ToString(), appType) + "'> Click Here </a>" +
                    "<br/>" +
                    "Download Zip File: <a href='" + GetDownloadLinkForFile(appDto.AppId.ToString(), appType) + "'> Click Here </a> <br/>";

                var responseText = GetApplicationDetailsStringHtml(this, DetailViewNames.ViewName(appType), appDto);

                mailBody += responseText;

                var mailTo = Settings.Default.ToEmailAddress;
                Task.Run(() => CommunicationHelper.SendEmailToAdminAsync(mailSubject, mailBody, mailTo, appDto.Email, appDto.NameApplicant, FilePathList));

                // Send mail to applicant
                mailSubject = "Insentivordning submitted successfully";
                mailBody = MailTemplate.GetMailBodyForApplicant(appType);

                Task.Run(() => CommunicationHelper.SendConfirmationEmailToUserAsync(mailSubject, mailBody, appDto.Email));
                Task.Run(() => CommunicationHelper.SendConfirmationEmailToUserAsync(mailSubject, mailBody, appDto.EmailContactInfo));

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