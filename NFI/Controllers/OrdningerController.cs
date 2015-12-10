using System;
using System.Linq;
using System.Web.Mvc;
using NFI.App_Start;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;
using NFI.Properties;

namespace NFI.Controllers
{
    public class OrdningerController : BaseController
    {
        // GET: Ordninger
        [CaptchaAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(OrdningerDto appDto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Status"] = "Error";
                return View("Index");
            }
            try
            {
                var appType = ApplicationType.Ordninger;
                SaveApplication(appDto, appType, appDto.Prosjektetstittel);

                //TODO: Send the mails
                // Send mail to archivist 
                var mailSubject = $"{appDto.Prosjektetstittel}  {appDto.Prosjektetstittel}";
                var mailBody = "A new application has been submitted.<br/>Application Details: <a href='" + GetDetailViewLink(appDto.AppId.ToString(), appType) + "'> Click Here </a> ";
                mailBody += "<br/>" +
                               "Download Zip File: <a href='" + GetDownloadLinkForFile(appDto.AppId.ToString(), appType) + "'> Click Here </a>";
                var responseText = GetApplicationDetailsStringHtml(this, DetailViewNames.ViewName(appType), appDto);
                mailBody += responseText;
                var mailTo = Settings.Default.ToEmailAddress;
                CommunicationHelper.SendEmail(mailSubject, mailBody, mailTo, FilePathList);

                // Send mail to applicant
                mailSubject = "3 Ordninger søknad sendtt";
                mailBody = MailTemplate.GetMailBodyForApplicant(ApplicationType.Ordninger);
                CommunicationHelper.SendEmail(mailSubject, mailBody, appDto.Epostadressekontaktperson);

                return View("Success");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}