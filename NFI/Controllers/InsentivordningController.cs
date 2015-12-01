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

                var files = new List<string>
                {
                    SaveUploadedFile(appDto.LeggCertificateOriginForHovedproduksjonsselskap),
                    SaveUploadedFile(appDto.LeggHovedprodusentensCv),
                    SaveUploadedFile(appDto.LeggHovedproduksjonsselskapetsTrackRecord),
                    SaveUploadedFile(appDto.LastoppErklæring),
                    SaveUploadedFile(appDto.LeggvedDokumentasjonHovedprodusenten),
                    SaveUploadedFile(appDto.LeggvedUtfyltkulturProduksjonstest),
                    SaveUploadedFile(appDto.LeggvedManuskript),
                    SaveUploadedFile(appDto.LeggvedTreatment),
                    SaveUploadedFile(appDto.LeggvedProduksjonsplan),
                    SaveUploadedFile(appDto.LeggvedCastCrewListe),
                    SaveUploadedFile(appDto.LeggvedListeOverLocations),
                    SaveUploadedFile(appDto.LeggvedListeOverLeverandører),
                    SaveUploadedFile(appDto.LeggvedDistribusjonsPlan),
                    SaveUploadedFile(appDto.LeggvedTotalbudsjettet),
                    SaveUploadedFile(appDto.LeggvedBudsjettForProduksjonen),
                    SaveUploadedFile(appDto.LeggvedFinansieringsplan)
                };

                files.AddRange(appDto.HarduVedleggSomerRelevante.Select(SaveUploadedFile));
                files = files.Where(x => x != null).ToList();

                files.Add(CreateUserDataFile(appDto)); // User data file

                var zipFilePath = DirectoryHelper.GetZipFilePath(appType, appDto.AppId, appDto.ProduksjonsforetaketsNavn);
                var zipFilePhysicalPath = Server.MapPath(zipFilePath);

                ZipHelper.CreateZipFromFiles(files, zipFilePhysicalPath);

                var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(appType);
                JsonHelper.Save<InsentivordningDto>(appDto, Server.MapPath(dataFilePath));
                
                //TODO: Send the mails

            }
            catch(Exception ex)
            {
                throw ex;
            }

            return true;
        }
    }
}