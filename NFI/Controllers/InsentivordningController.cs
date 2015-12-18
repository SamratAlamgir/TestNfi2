using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using NFI.App_Start;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;
using NFI.Properties;

namespace NFI.Controllers
{
    public class InsentivordningController : BaseController
    {
        [CaptchaAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(InsentivordningDto appDto)
        {
            try
            {
                var appType = ApplicationType.Insentivordning;
                var mailSubject = "INSENTIVORDNING " + appDto.TittelpåProsjektet;

                SaveApplication(appDto, appType, appDto.ProduksjonsforetaketsNavn, mailSubject);

                // Send mail to archivist
                
                var mailBody = "Hi,<br/>A new application has been submitted.<br/><br/>" +
                    "Application Details: <a href = '" + GetDetailViewLink(appDto.AppId.ToString(), appType) + "'> Click Here </a>" +
                    "<br/>" +
                    "Download Zip File: <a href='" + GetDownloadLinkForFile(appDto.AppId.ToString(), appType) + "'> Click Here </a> <br/>";

                var responseText = GetApplicationDetailsStringHtml(this, "../Admin/InsentivordningDetail", appDto);

                mailBody += responseText;

                var mailTo = Settings.Default.ToEmailAddress;
                Task.Run(() => CommunicationHelper.SendEmailToAdminAsync(mailSubject, mailBody, mailTo, appDto.HovedprodusentensEpostadresse, appDto.ProduksjonsforetaketsNavn, FilePathList));

                // Send mail to applicant
                mailSubject = "Insentivordning søknad sendt";
                mailBody = MailTemplate.GetMailBodyForApplicant(ApplicationType.Insentivordning);

                Task.Run(() => CommunicationHelper.SendConfirmationEmailToUserAsync(mailSubject, mailBody, appDto.HovedprodusentensEpostadresse));
                
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