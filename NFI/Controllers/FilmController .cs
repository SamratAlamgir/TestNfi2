using System;
using System.Web.Mvc;
using NFI.App_Start;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;
using NFI.Properties;

namespace NFI.Controllers
{
    public class FilmController : BaseController
    {
        // GET: Ordninger
        //[CaptchaAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FilmDto appDto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Status"] = "Error";
                return View("Index");
            }
            try
            {
                var appType = ApplicationType.Film;
                var mailSubject = $"TILSKUDD TIL FILMDISTRIBUSJON {appDto.Hvasøkesdet}  {appDto.Prosjektetstittel}";
                SaveApplication(appDto, appType, appDto.Prosjektetstittel, mailSubject);
                // Send mail to archivist 
                var mailBody = "A new application has been submitted.<br/>Application Details: <a href='" + GetDetailViewLink(appDto.AppId.ToString(), appType) + "'> Click Here </a> ";
                mailBody += "<br/>" +
                               "Download Zip File: <a href='" + GetDownloadLinkForFile(appDto.AppId.ToString(), appType) + "'> Click Here </a>";
                var responseText = GetApplicationDetailsStringHtml(this, DetailViewNames.ViewName(appType), appDto);
                mailBody += responseText;
                var mailTo = Settings.Default.ToEmailAddress;
                CommunicationHelper.SendEmailToAdmin(mailSubject, mailBody, mailTo, appDto.Epostadressekontaktperson, appDto.Epostadressekontaktperson, FilePathList);

                // Send mail to applicant
                mailSubject = "Film søknad sendt";
                mailBody = MailTemplate.GetMailBodyForApplicant(ApplicationType.Film);
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