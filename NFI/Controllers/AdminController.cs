using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;
using System.Collections.Generic;

namespace NFI.Controllers
{
    [BasicAuthenticationAttribute("admin", "test123", BasicRealm = "admin")]
    public class AdminController : BaseController
    {

        public ActionResult Index()
        {
            return View("ApplicationList");
        }

        public ActionResult ApplicationList()
        {
            return View("ApplicationList");
        }

        public JsonResult GetApplications(ApplicationType appType, bool includeArchive)
        {

            var result = new List<AdminListDto>();

            if (appType == ApplicationType.All)
            {
                result = GetSorfondDtoList();
                result.AddRange(GetInsentivordningDtoList());
                result.AddRange(GetIncentiveSchemeDtoList());
                result.AddRange(GetUdsReisestotteDtoList());
                result.AddRange(GetLanseringDtoList());
                result.AddRange(GetOrdningerDtoList());
                result.AddRange(GetVideoDtoList());
                result.AddRange(GetFilmDtoList());
            }
            else
            {
                result = GetApplicationList(appType);
            }

            if (!includeArchive)
            {
                result = result.Where(x => !x.IsArchived).ToList();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private List<AdminListDto> GetApplicationList(ApplicationType appType)
        {
            var result = new List<AdminListDto>();

            switch (appType)
            {
                case ApplicationType.Sorfond:
                    result = GetSorfondDtoList();
                    break;

                case ApplicationType.Insentivordning:
                    result = GetInsentivordningDtoList();
                    break;

                case ApplicationType.IncentiveScheme:
                    result = GetIncentiveSchemeDtoList();
                    break;

                case ApplicationType.UdsReisestotte:
                    result = GetUdsReisestotteDtoList();
                    break;

                case ApplicationType.Lansering:
                    result = GetLanseringDtoList();
                    break;

                case ApplicationType.Ordninger:
                    result = GetOrdningerDtoList();
                    break;

                case ApplicationType.Video:
                    result = GetVideoDtoList();
                    break;

                case ApplicationType.Film:
                    result = GetFilmDtoList();
                    break;
            }

            return result;
        }

        // Get Sorfond data
        private List<AdminListDto> GetSorfondDtoList()
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(ApplicationType.Sorfond);

            var result = JsonHelper.GetCollections<SorfondDto>(dataFilePath)
                .Select(x => new AdminListDto()
                {
                    AppId = x.AppId,
                    AppType = "Sørfond",
                    AppTypeId = ApplicationType.Sorfond,
                    ApplicantName = x.NorskMinoritetsprodusent.MinoritetsprodusentensNavn,
                    Email = x.NorskMinoritetsprodusent.MinoritetsprodusentensEpostadresse,
                    CreateTime = x.CreateTime,
                    IsArchived = x.IsArchived
                });

            return result.ToList();
        }

        // Get Insentivordning data
        private List<AdminListDto> GetInsentivordningDtoList()
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(ApplicationType.Insentivordning);

            var result = JsonHelper.GetCollections<InsentivordningDto>(dataFilePath)
                .Select(x => new AdminListDto()
                {
                    AppId = x.AppId,
                    AppType = "Insentivordning",
                    AppTypeId = ApplicationType.Insentivordning,
                    ApplicantName = x.HovedprodusentensNavn,
                    Email = x.HovedprodusentensEpostadresse,
                    CreateTime = x.CreateTime,
                    IsArchived = x.IsArchived
                });

            return result.ToList();
        }

        // Get IncentiveScheme data
        private List<AdminListDto> GetIncentiveSchemeDtoList()
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(ApplicationType.IncentiveScheme);

            var result = JsonHelper.GetCollections<IncentiveSchemeDto>(dataFilePath)
                .Select(x => new AdminListDto()
                {
                    AppId = x.AppId,
                    AppType = "Incentive Scheme",
                    AppTypeId = ApplicationType.IncentiveScheme,
                    ApplicantName = x.NameProducer,
                    Email = x.Email,
                    CreateTime = x.CreateTime,
                    IsArchived = x.IsArchived
                });

            return result.ToList();
        }

        // Get UDs reisestøtte data
        private List<AdminListDto> GetUdsReisestotteDtoList()
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(ApplicationType.UdsReisestotte);

            var result = JsonHelper.GetCollections<UdsReisestotteDto>(dataFilePath)
                .Select(x => new AdminListDto()
                {
                    AppId = x.AppId,
                    AppType = "UDs reisestøtte",
                    AppTypeId = ApplicationType.UdsReisestotte,
                    ApplicantName = x.Søkersnavn,
                    Email = x.Søkersepost,
                    CreateTime = x.CreateTime,
                    IsArchived = x.IsArchived
                });

            return result.ToList();
        }

        // Get Lansering data
        private List<AdminListDto> GetLanseringDtoList()
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(ApplicationType.Lansering);

            var result = JsonHelper.GetCollections<LanseringDto>(dataFilePath)
                .Select(x => new AdminListDto()
                {
                    AppId = x.AppId,
                    AppType = "Lansering",
                    AppTypeId = ApplicationType.Lansering,
                    ApplicantName = "Need to be added", //TODO: Need to be added
                    Email = x.EpostadresseKontaktperson,
                    CreateTime = x.CreateTime,
                    IsArchived = x.IsArchived
                });


            return result.ToList();
        }


        // Get Ordninger data
        private List<AdminListDto> GetOrdningerDtoList()
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(ApplicationType.Ordninger);

            var result = JsonHelper.GetCollections<OrdningerDto>(dataFilePath)
                .Select(x => new AdminListDto()
                {
                    AppId = x.AppId,
                    AppType = "Ordninger",
                    AppTypeId = ApplicationType.Ordninger,
                    ApplicantName = x.Navnpåkontaktperson,
                    Email = x.Epostadressekontaktperson,
                    CreateTime = x.CreateTime,
                    IsArchived = x.IsArchived
                });

            return result.ToList();
        }

        // Get Video data
        private List<AdminListDto> GetVideoDtoList()
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(ApplicationType.Video);

            var result = JsonHelper.GetCollections<VideoDto>(dataFilePath)
                .Select(x => new AdminListDto()
                {
                    AppId = x.AppId,
                    AppType = "Video",
                    AppTypeId = ApplicationType.Video,
                    ApplicantName = x.NavnKontaktpersonDenneSøknaden,
                    Email = x.Epostadressekontaktperson,
                    CreateTime = x.CreateTime,
                    IsArchived = x.IsArchived
                });

            return result.ToList();
        }

        // Get Film data
        private List<AdminListDto> GetFilmDtoList()
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(ApplicationType.Film);

            var result = JsonHelper.GetCollections<FilmDto>(dataFilePath)
                .Select(x => new AdminListDto()
                {
                    AppId = x.AppId,
                    AppType = "Film",
                    AppTypeId = ApplicationType.Film,
                    ApplicantName = x.Navnpåkontaktperson,
                    Email = x.Epostadressekontaktperson,
                    CreateTime = x.CreateTime,
                    IsArchived = x.IsArchived
                });

            return result.ToList();
        }

        /*
        // Get Den Kulturelle Skolesekken data
        private List<AdminListDto> GetFilmDtoList(string dataFilePath)
        {
            return result.ToList();
        }
        */

        public bool MarkAsArchive(ApplicationType appType, string appId)
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(appType);
            dynamic resultSet;

            switch (appType)
            {
                case ApplicationType.Sorfond:
                    resultSet = JsonHelper.GetCollections<SorfondDto>(dataFilePath);
                    MarkAsArchiveAndSave<SorfondDto>(resultSet, appId, dataFilePath);
                    break;
                case ApplicationType.Insentivordning:
                    resultSet = JsonHelper.GetCollections<InsentivordningDto>(dataFilePath);
                    MarkAsArchiveAndSave<InsentivordningDto>(resultSet, appId, dataFilePath);
                    break;

                case ApplicationType.IncentiveScheme:
                    resultSet = JsonHelper.GetCollections<IncentiveSchemeDto>(dataFilePath);
                    MarkAsArchiveAndSave<IncentiveSchemeDto>(resultSet, appId, dataFilePath);
                    break;
                case ApplicationType.UdsReisestotte:
                    resultSet = JsonHelper.GetCollections<UdsReisestotteDto>(dataFilePath);
                    MarkAsArchiveAndSave<UdsReisestotteDto>(resultSet, appId, dataFilePath);
                    break;
                case ApplicationType.Lansering:
                    resultSet = JsonHelper.GetCollections<LanseringDto>(dataFilePath);
                    MarkAsArchiveAndSave<LanseringDto>(resultSet, appId, dataFilePath);
                    break;
                case ApplicationType.Ordninger:
                    resultSet = JsonHelper.GetCollections<OrdningerDto>(dataFilePath);
                    MarkAsArchiveAndSave<OrdningerDto>(resultSet, appId, dataFilePath);
                    break;
                case ApplicationType.Video:
                    resultSet = JsonHelper.GetCollections<VideoDto>(dataFilePath);
                    MarkAsArchiveAndSave<VideoDto>(resultSet, appId, dataFilePath);
                    break;
                case ApplicationType.Film:
                    resultSet = JsonHelper.GetCollections<FilmDto>(dataFilePath);
                    MarkAsArchiveAndSave<FilmDto>(resultSet, appId, dataFilePath);
                    break;
            }

            return true;
        }

        private bool MarkAsArchiveAndSave<T>(List<T> resultSet, string appId, string dataFilePath) where T : BaseAppDto
        {
            var selectedApp = resultSet.Single(x => x.AppId.ToString() == appId);
            selectedApp.IsArchived = true;

            JsonHelper.Save(resultSet, dataFilePath);

            return true;
        }

        public ActionResult ShowDetail(ApplicationType appType, string appId)
        {
            try
            {
                var viewName = DetailViewNames.ViewName(appType);
                object selectedApp = GetBaseAppDto(appType, appId);
                TrimPathAndOnlyFileName(selectedApp);
                return View(viewName, selectedApp);
            }
            catch (Exception ex)
            {
                LogWriter.Write(ex.ToString(), "Error");
                return View("Error");
            }
        }

        public ActionResult DownloadZipFile(ApplicationType appType, string appId)
        {
            try
            {
                var selectedApp = GetBaseAppDto(appType, appId);
                var filePath = selectedApp?.ZipFilePath;
                filePath = filePath.Replace(@"\Admin\DownloadZipFile", "");
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                var fileName = Path.GetFileName(filePath);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception ex)
            {
                LogWriter.Write(ex.ToString(), "Error");
                return View("Error");
            }

        }

        private BaseAppDto GetBaseAppDto(ApplicationType appType, string appId)
        {
            BaseAppDto selectedApp = null;
            switch (appType)
            {
                case ApplicationType.Insentivordning:
                    selectedApp = GetApplicationDto<InsentivordningDto>(appId, appType);
                    break;
                case ApplicationType.IncentiveScheme:
                    selectedApp = GetApplicationDto<IncentiveSchemeDto>(appId, appType);
                    break;
                case ApplicationType.Sorfond:
                    selectedApp = GetApplicationDto<SorfondDto>(appId, appType);
                    break;
                case ApplicationType.UdsReisestotte:
                    selectedApp = GetApplicationDto<UdsReisestotteDto>(appId, appType);
                    break;
                case ApplicationType.Lansering:
                    selectedApp = GetApplicationDto<LanseringDto>(appId, appType);
                    break;
                case ApplicationType.Ordninger:
                    selectedApp = GetApplicationDto<OrdningerDto>(appId, appType);
                    break;
                case ApplicationType.Film:
                    selectedApp = GetApplicationDto<FilmDto>(appId, appType);
                    break;
                case ApplicationType.Video:
                    selectedApp = GetApplicationDto<VideoDto>(appId, appType);
                    break;
            }
            return selectedApp;
        }

        private T GetApplicationDto<T>(string appId, ApplicationType appType)
            where T : BaseAppDto
        {
            var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(appType);
            var resultSet = JsonHelper.GetCollections<T>(dataFilePath);

            return resultSet.Single(x => x.AppId.ToString() == appId);
        }

    }
}
