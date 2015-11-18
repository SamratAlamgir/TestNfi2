using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}