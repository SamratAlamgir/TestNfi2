using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;

namespace NFI.Controllers
{
    public class SorfondController : BaseController
    {
        private List<string> _filePathList = new List<string>();
        // GET: Sorfond
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
            //if (!ModelState.IsValid)
            //{
            //    return View("Error");
            //}
            try
            {
                var appType = ApplicationType.Sorfond;
                sorfondDto.AppId = Guid.NewGuid();
                _filePathList = new List<string>();

                SaveFilesAndSetFilePath(sorfondDto);

                // User data file
                _filePathList = _filePathList.Where(x => !string.IsNullOrEmpty(x)).ToList();
                var zipFilePath = DirectoryHelper.GetZipFilePath(appType, sorfondDto.AppId, sorfondDto.HovedProdusent.HovedprodusentProduksjonsforetaketsNavn);
                sorfondDto.ZipFilePath = ".." + zipFilePath;

                var zipFilePhysicalPath = Server.MapPath(zipFilePath);
                ZipHelper.CreateZipFromFiles(_filePathList, zipFilePhysicalPath);

                var dataFilePath = DirectoryHelper.GetApplicationDataFilePath(appType);
                JsonHelper.Save<SorfondDto>(sorfondDto, Server.MapPath(dataFilePath));

                return View("Success");
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }

        private void SaveFilesAndSetFilePath(Object obj)
        {
            var type = obj.GetType();
            var fieldInfos =
                type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(f => NotPrimitive(f.PropertyType))
                    .ToList();
            var allFieldPaths = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(f => f.Name.Contains("Path"))
                    .ToList();
            foreach (var fieldInfo in fieldInfos)
            {
                if (fieldInfo.PropertyType == typeof(HttpPostedFileBase))
                {
                    var filePath = SaveUploadedFile((HttpPostedFileBase)fieldInfo.GetValue(obj));
                    _filePathList.Add(filePath);
                    var fieldPath = allFieldPaths.FirstOrDefault(c => c.Name == fieldInfo.Name + "Path");
                    fieldPath?.SetValue(obj, filePath);
                }
                else if (fieldInfo.PropertyType.IsGenericType
                         && fieldInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
                         && fieldInfo.PropertyType.GetGenericArguments()[0] == typeof(HttpPostedFileBase))
                {
                    var files = (List<HttpPostedFileBase>)fieldInfo.GetValue(obj);
                    if (files != null)
                    {
                        var filePaths = files.Select(SaveUploadedFile).ToList();
                        _filePathList.AddRange(filePaths);
                        var fieldPath = allFieldPaths.FirstOrDefault(c => c.Name == fieldInfo.Name + "Paths");
                        fieldPath?.SetValue(obj, filePaths);
                    }
                }
                else if (fieldInfo.PropertyType.IsGenericType &&
                         fieldInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
                         && fieldInfo.PropertyType.GetGenericArguments()[0].GetInterfaces().Contains(typeof(IMember)))
                {

                    var objs = fieldInfo.GetValue(obj) as IList;
                    if (objs != null)
                    {
                        foreach (var member in objs)
                        {
                            SaveFilesAndSetFilePath(member);
                        }
                    }
                }

                else if (fieldInfo.PropertyType.GetInterfaces().Contains(typeof(IMember)))
                {
                    var o = fieldInfo.GetValue(obj);
                    if (o != null)
                        SaveFilesAndSetFilePath(o);
                }

            }
        }

        private static bool NotPrimitive(Type type)
        {
            return !(type.IsPrimitive || type == typeof(Guid)
                     || type == typeof(string));
        }
    }
}
