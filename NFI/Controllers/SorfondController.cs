﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NFI.App_Start;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;
using NFI.Properties;

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
                return View("Error");
            }
            try
            {
                var appType = ApplicationType.Sorfond;
                sorfondDto.AppId = Guid.NewGuid();
                sorfondDto.CreateTime = DateTime.Now;
                FilePathList = new List<string>();

                SaveFilesAndSetFilePath(sorfondDto);

                // User data file
                FilePathList = FilePathList.Where(x => !string.IsNullOrEmpty(x)).ToList();
                var zipFilePath = DirectoryHelper.GetZipFilePath(appType, sorfondDto.AppId, sorfondDto.Prosjektinformasjon.TittelPåProsjektet);
                sorfondDto.ZipFilePath = zipFilePath;

                var zipFilePhysicalPath = zipFilePath;
                ZipHelper.CreateZipFromFiles(FilePathList, zipFilePhysicalPath);

                var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(appType);
                JsonHelper.Save<SorfondDto>(sorfondDto, dataFilePath);

                //TODO: Send the mails
                var mailSubject = "SØRFOND " + sorfondDto.Prosjektinformasjon.TittelPåProsjektet;
                var mailBody = "A new application has been submitted.<br/>Application Details: <a href='" + GetDetailViewLink(sorfondDto.AppId.ToString(), appType) + "'> Click Here </a> ";
                mailBody += "<br/>" +
                               "Download Zip File: <a href='" + GetDownloadLinkForFile(sorfondDto.AppId.ToString(), appType) + "'> Click Here </a>";

                var mailTo = Settings.Default.ToEmailAddress;
                CommunicationHelper.SendMailToExecutive(mailSubject, mailBody, mailTo);

                return View("Success");
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }


    }
}
