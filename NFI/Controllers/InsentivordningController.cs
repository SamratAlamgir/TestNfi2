using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;
using NFI.Properties;

namespace NFI.Controllers
{
    public class InsentivordningController : BaseController
    {
        // GET: Insentivordning
        public ActionResult Index()
        {
            return View();
        }

        public bool Save(InsentivordningDto appDto)
        {
            try
            {
                var appType = ApplicationType.Insentivordning;
                appDto.AppId = Guid.NewGuid();
                appDto.CreateTime = DateTime.Now;

                var files = new List<string>();

                files.Add(appDto.LeggCertificateOriginForHovedproduksjonsselskapPath = SaveUploadedFile(appDto.LeggCertificateOriginForHovedproduksjonsselskap, appType));
                files.Add(appDto.LeggHovedprodusentensCvPath = SaveUploadedFile(appDto.LeggHovedprodusentensCv, appType));
                files.Add(appDto.LeggHovedproduksjonsselskapetsTrackRecordPath = SaveUploadedFile(appDto.LeggHovedproduksjonsselskapetsTrackRecord, appType));
                files.Add(appDto.LastoppErklæringPath = SaveUploadedFile(appDto.LastoppErklæring, appType));
                files.Add(appDto.LeggvedDokumentasjonHovedprodusentenPath = SaveUploadedFile(appDto.LeggvedDokumentasjonHovedprodusenten, appType));
                files.Add(appDto.LeggvedUtfyltkulturProduksjonstestPath = SaveUploadedFile(appDto.LeggvedUtfyltkulturProduksjonstest, appType));
                files.Add(appDto.LeggvedManuskriptPath = SaveUploadedFile(appDto.LeggvedManuskript, appType));
                files.Add(appDto.LeggvedTreatmentPath = SaveUploadedFile(appDto.LeggvedTreatment, appType));
                files.Add(appDto.LeggvedProduksjonsplanPath = SaveUploadedFile(appDto.LeggvedProduksjonsplan, appType));
                files.Add(appDto.LeggvedCastCrewListePath = SaveUploadedFile(appDto.LeggvedCastCrewListe, appType));
                files.Add(appDto.LeggvedListeOverLocationsPath = SaveUploadedFile(appDto.LeggvedListeOverLocations, appType));
                files.Add(appDto.LeggvedListeOverLeverandørerPath = SaveUploadedFile(appDto.LeggvedListeOverLeverandører, appType));
                files.Add(appDto.LeggvedDistribusjonsPlanPath = SaveUploadedFile(appDto.LeggvedDistribusjonsPlan, appType));
                files.Add(appDto.LeggvedTotalbudsjettetPath = SaveUploadedFile(appDto.LeggvedTotalbudsjettet, appType));
                files.Add(appDto.LeggvedBudsjettForProduksjonenPath = SaveUploadedFile(appDto.LeggvedBudsjettForProduksjonen, appType));
                files.Add(appDto.LeggvedFinansieringsplanPath = SaveUploadedFile(appDto.LeggvedFinansieringsplan, appType));

                files.AddRange(appDto.HarduVedleggSomerRelevantePaths = appDto.HarduVedleggSomerRelevante.Select(x => SaveUploadedFile(x, appType)).ToList());
                files = files.Where(x => x != null).ToList();

                files.Add(CreateTextFile(appDto, appType)); // User data file

                var zipFilePath = DirectoryHelper.GetZipFilePath(appType, appDto.AppId, appDto.ProduksjonsforetaketsNavn);
                appDto.ZipFilePath = ".." + zipFilePath;

                var zipFilePhysicalPath = Server.MapPath(zipFilePath);
                ZipHelper.CreateZipFromFiles(files, zipFilePhysicalPath);

                var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(appType);
                JsonHelper.Save<InsentivordningDto>(appDto, Server.MapPath(dataFilePath));

                //TODO: Send the mails
                var mailSubject = "INSENTIVORDNING " + appDto.TittelpåProsjektet;
                var mailBody = "A new application has been submitted.<br/>" +
                               "Download Zip File: <a href='" + GetDownloadLinkForFile(appDto.AppId.ToString(), appType) + "'> Click Here </a>";
                               
                var mailTo = Settings.Default.ToEmailAddress;
                CommunicationHelper.SendMailToExecutive(mailSubject, mailBody, mailTo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
    }
}