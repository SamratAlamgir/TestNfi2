﻿using System;
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
                SaveApplication(appDto, appType, appDto.ProduksjonsforetaketsNavn);

                // Send mail to archivist
                var mailSubject = "INSENTIVORDNING " + appDto.TittelpåProsjektet;
                var mailBody = "Hi,<br/>A new application has been submitted.<br/><br/>" +
                    "Application Details: <a href = '" + GetDetailViewLink(appDto.AppId.ToString(), appType) + "'> Click Here </a>" +
                    "<br/>" +
                    "Download Zip File: <a href='" + GetDownloadLinkForFile(appDto.AppId.ToString(), appType) + "'> Click Here </a> <br/>";

                var responseText = GetApplicationDetailsStringHtml(this, "../Admin/InsentivordningDetail", appDto);

                mailBody += responseText;

                var mailTo = Settings.Default.ToEmailAddress;
                CommunicationHelper.SendEmail(mailSubject, mailBody, mailTo, FilePathList);

                // Send mail to applicant
                mailSubject = "Insentivordning søknad sendtt";
                mailBody = MailTemplate.GetMailBodyForApplicant(ApplicationType.Insentivordning);

                CommunicationHelper.SendEmail(mailSubject, mailBody, appDto.SøkersEpostAdresse);

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