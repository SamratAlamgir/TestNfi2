﻿using System;
using System.Linq;
using System.Web.Mvc;
using NFI.App_Start;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;

namespace NFI.Controllers
{
    public class SorfondController : BaseController
    {
        // GET: Sorfond
        [CaptchaAuthorize]
        public ActionResult Index()
        {
            var model = new SorfondDto();
            model.InformasjonOmPersonerRoller.Regissoren.Add(new Regissoren());
            model.InformasjonOmPersonerRoller.Manusforfatterens.Add(new Manusforfatterens());
            model.VisueltMateriale.NettadresseVisueltMateriale.Add(new NettadresseVisueltMateriale());
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(SorfondDto sorfondDto)
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values.SelectMany(v => v.Errors).ToList();
                foreach (var error in erros)
                {
                    LogWriter.Write(error.Exception.Message, "Error");
                }
                
                return View("Error");
            }
            try
            {
                var appType = ApplicationType.Sorfond;
                var mailSubject = "Sørfond " + sorfondDto.Prosjektinformasjon.TittelPåProsjektet;

                SaveApplication(sorfondDto, appType, sorfondDto.Prosjektinformasjon.TittelPåProsjektet, mailSubject);
                
                // Send the mails
                var mailBody = MailTemplate.GetMailBodyForAdmin(sorfondDto.AppId, appType);

                var responseText = GetApplicationDetailsStringHtml(this, DetailViewNames.ViewName(appType), sorfondDto);
                mailBody += responseText;

                CommunicationHelper.SendEmailToAdmin(mailSubject, mailBody, "sorfond@nfi.no", 
                    sorfondDto.NorskMinoritetsprodusent.MinoritetsprodusentensEpostadresse, sorfondDto.NorskMinoritetsprodusent.MinoritetsprodusentensEpostadresse, FilePathList);

                // Send mail to applicant
                mailSubject = "Sørfond søknad sendt";
                mailBody = MailTemplate.GetMailBodyForApplicant(appType);

                CommunicationHelper.SendConfirmationEmailToUser(mailSubject, mailBody, sorfondDto.NorskMinoritetsprodusent.MinoritetsprodusentensEpostadresse);

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
