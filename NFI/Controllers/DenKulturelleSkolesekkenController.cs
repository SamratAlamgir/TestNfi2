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
    public class DenKulturelleSkolesekkenController : BaseController
    {
        // GET: Ordninger
        [CaptchaAuthorize]
        public ActionResult Index()
        {
            var model = new DenKulturelleSkolesekkenDto();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(DenKulturelleSkolesekkenDto appDto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Status"] = "Error";
                return View("Index");
            }
            try
            {
                var appType = ApplicationType.DenKulturelleSkolesekken;
                var mailSubject = "DEN KULTURELLE SKOLESEKKEN " + appDto.Harprosjektet.Aggregate((i, j) => i + ", " + j) + appDto.Prosjektetstittel;
                SaveApplication(appDto, appType, appDto.Prosjektetstittel, mailSubject);
                // Send mail to archivist 
                var mailBody = MailTemplate.GetMailBodyForAdmin(appDto.AppId, appType);

                var responseText = GetApplicationDetailsStringHtml(this, DetailViewNames.ViewName(appType), appDto);

                mailBody += responseText;
                var mailTo = Settings.Default.ToEmailAddress;
                CommunicationHelper.SendEmailToAdmin(mailSubject, mailBody, mailTo, appDto.Epostadressekontaktperson, appDto.Epostadressekontaktperson, FilePathList);

                // Send mail to applicant
                mailSubject = "DEN KULTURELLE SKOLESEKKEN søknad sendt";
                mailBody = MailTemplate.GetMailBodyForApplicant(appType);
                CommunicationHelper.SendConfirmationEmailToUser(mailSubject, mailBody, appDto.Epostadressekontaktperson);

                return View("Success");
            }
            catch (Exception ex)
            {
                LogWriter.Write(ex.ToString(), "Error");
                return View("Error");
            }
        }
    }
}