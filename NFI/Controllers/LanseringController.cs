using System;
using System.Web.Mvc;
using NFI.App_Start;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;
using NFI.Properties;

namespace NFI.Controllers
{
    public class LanseringController : BaseController
    {

        [CaptchaAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(LanseringDto appDto)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            try
            {
                var appType = ApplicationType.Lansering;
                var mailSubject = $"Felles lanseringstiltak på viktige internasjonale arenaer {appDto.PåhvilkenArena}  {appDto.NavnpåAnsvarligOrganisasjon}";

                SaveApplication(appDto, appType, appDto.ProsjektetsTittel, mailSubject);

                //TODO: Send the mails
                // Send mail to archivist 
                var mailBody = MailTemplate.GetMailBodyForAdmin();

                var responseText = GetApplicationDetailsStringHtml(this, DetailViewNames.ViewName(appType), appDto);
                mailBody += responseText;
                var mailTo = Settings.Default.ToEmailAddress;
                CommunicationHelper.SendEmailToAdmin(mailSubject, mailBody, mailTo, appDto.EpostadresseKontaktperson, appDto.EpostadresseKontaktperson, FilePathList);
              

                // Send mail to applicant
                mailSubject = "Lansering søknad sendt";
                mailBody = MailTemplate.GetMailBodyForApplicant(appType);

                CommunicationHelper.SendConfirmationEmailToUser(mailSubject, mailBody, appDto.EpostadresseKontaktperson);
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