using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;

namespace NFI.Controllers
{
    public class InsentivordningController : BaseController
    {
        // GET: Insentivordning
        public ActionResult Insentivordning()
        {
            return View("Insentivordning");
        }

        public bool Save(InsentivordningDto appDto)
        {
            try
            {
                var appType = ApplicationType.Insentivordning;
                appDto.AppId = Guid.NewGuid();
                appDto.CreateTime = DateTime.Now;

                var files = new List<string>();

                files.Add(appDto.LeggCertificateOriginForHovedproduksjonsselskapPath = SaveUploadedFile(appDto.LeggCertificateOriginForHovedproduksjonsselskap));
                files.Add(appDto.LeggHovedprodusentensCvPath = SaveUploadedFile(appDto.LeggHovedprodusentensCv));
                files.Add(appDto.LeggHovedproduksjonsselskapetsTrackRecordPath = SaveUploadedFile(appDto.LeggHovedproduksjonsselskapetsTrackRecord));
                files.Add(appDto.LastoppErklæringPath = SaveUploadedFile(appDto.LastoppErklæring));
                files.Add(appDto.LeggvedDokumentasjonHovedprodusentenPath = SaveUploadedFile(appDto.LeggvedDokumentasjonHovedprodusenten));
                files.Add(appDto.LeggvedUtfyltkulturProduksjonstestPath = SaveUploadedFile(appDto.LeggvedUtfyltkulturProduksjonstest));
                files.Add(appDto.LeggvedManuskriptPath = SaveUploadedFile(appDto.LeggvedManuskript));
                files.Add(appDto.LeggvedTreatmentPath = SaveUploadedFile(appDto.LeggvedTreatment));
                files.Add(appDto.LeggvedProduksjonsplanPath = SaveUploadedFile(appDto.LeggvedProduksjonsplan));
                files.Add(appDto.LeggvedCastCrewListePath = SaveUploadedFile(appDto.LeggvedCastCrewListe));
                files.Add(appDto.LeggvedListeOverLocationsPath = SaveUploadedFile(appDto.LeggvedListeOverLocations));
                files.Add(appDto.LeggvedListeOverLeverandørerPath = SaveUploadedFile(appDto.LeggvedListeOverLeverandører));
                files.Add(appDto.LeggvedDistribusjonsPlanPath = SaveUploadedFile(appDto.LeggvedDistribusjonsPlan));
                files.Add(appDto.LeggvedTotalbudsjettetPath = SaveUploadedFile(appDto.LeggvedTotalbudsjettet));
                files.Add(appDto.LeggvedBudsjettForProduksjonenPath = SaveUploadedFile(appDto.LeggvedBudsjettForProduksjonen));
                files.Add(appDto.LeggvedFinansieringsplanPath = SaveUploadedFile(appDto.LeggvedFinansieringsplan));

                files.AddRange(appDto.HarduVedleggSomerRelevante.Select(SaveUploadedFile));
                files = files.Where(x => x != null).ToList();

                files.Add(CreateUserDataFile(appDto)); // User data file

                var zipFilePath = DirectoryHelper.GetZipFilePath(appType, appDto.AppId, appDto.ProduksjonsforetaketsNavn);
                appDto.ZipFilePath = ".." + zipFilePath;

                var zipFilePhysicalPath = Server.MapPath(zipFilePath);
                ZipHelper.CreateZipFromFiles(files, zipFilePhysicalPath);

                var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(appType);
                JsonHelper.Save<InsentivordningDto>(appDto, Server.MapPath(dataFilePath));

                //TODO: Send the mails

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
    }
}