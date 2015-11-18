using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFI.Enums;
using NFI.Helper;
using NFI.Models;

namespace NFI.Controller
{
    public class AdminController : System.Web.Mvc.Controller
    {

        public ActionResult Index()
        {
            return View("ApplicationList");
        }

        public ActionResult ApplicationList()
        {
            return View("ApplicationList");
        }

        public JsonResult GetApplications()
        {
            var result = JsonHelper.GetCollections<Application1Dto>(ApplicationType.Application1);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
