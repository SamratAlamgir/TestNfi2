using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFI.Models;

namespace NFI.Controllers
{
    public class InsentivordningController : Controller
    {
        // GET: Insentivordning
        public ActionResult Insentivordning()
        {
            return View("Insentivordning");
        }

        public bool Save(InsentivordningDto formDto)
        {

            return true;
        }
    }
}