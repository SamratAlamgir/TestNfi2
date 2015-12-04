using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using NFI.App_Start;
using System.Web.Routing;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;
using NFI.Properties;

namespace NFI.Controllers
{
    [CaptchaAuthorize]
    public class InsentivordningController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Save(InsentivordningDto appDto)
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
                appDto.ZipFilePath = zipFilePath;

                var zipFilePhysicalPath = zipFilePath;
                ZipHelper.CreateZipFromFiles(files, zipFilePhysicalPath);

                var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(appType);
                JsonHelper.Save<InsentivordningDto>(appDto, dataFilePath);

                //TODO: Send the mails
                var mailSubject = "INSENTIVORDNING " + appDto.TittelpåProsjektet;
                var mailBody = "A new application has been submitted.<br/>" +
                    "Application Details: <a href = '" + GetDetailViewLink(appDto.AppId.ToString(), appType) + "'> Click Here </a>" +
                    "<br/>" +
                    "Download Zip File: <a href='" + GetDownloadLinkForFile(appDto.AppId.ToString(), appType) + "'> Click Here </a> <br/>";

                var responseText =  GetResponseHtml(ApplicationType.Insentivordning, appDto.AppId);

                mailBody += responseText;

                var mailTo = Settings.Default.ToEmailAddress;
                CommunicationHelper.SendMailToExecutive(mailSubject, mailBody, mailTo);

                Session["IsCaptchaVerfied"] = false;
                return View("Success");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex;
                return View("Error");
            }
        }

        public string GetResponseHtml(ApplicationType appType, Guid appId)
        {
            string output = string.Empty;

            RouteValueDictionary rvd = new RouteValueDictionary();
            rvd.Add("appType", appType);
            rvd.Add("appId", appId);

            WebRequest webRequest = WebRequest.Create(GetDetailViewLink(appId.ToString(), appType) );

            SetBasicAuthHeader(webRequest, "admin", "test123");

            WebResponse webResponse = webRequest.GetResponse();
            if (webResponse.GetResponseStream().CanRead)
            {
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                output = reader.ReadToEnd();
            }
            webResponse.Close();

            return output;
        }

        public void SetBasicAuthHeader(WebRequest request, String userName, String userPassword)
        {
            string authInfo = userName + ":" + userPassword;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
        }

    }
}