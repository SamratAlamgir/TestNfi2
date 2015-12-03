using System.Web.Mvc;
using NFI.Models;

namespace NFI.Controllers
{
    public class SorfondController : Controller
    {
        // GET: Sorfond
        public ActionResult Index()
        {
            var model = new Sorfond();
            model.InformasjonOmPersonerRoller.Regissoren.Add(new Regissoren());
            model.InformasjonOmPersonerRoller.Manusforfatterens.Add(new Manusforfatterens());
            model.VisueltMateriale.NettadresseVisueltMateriale.Add(new NettadresseVisueltMateriale());
            return View(model);
        }

      
        [HttpPost]
        public ActionResult Create(Sorfond sorfond)
        {

            return View("Error");
        }

        //// POST: Sorfond/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //   if (ModelState.IsValid)
        //        {
        //            // TODO: Add insert logic here

        //            return RedirectToAction("Index");
        //        }
        //    return View();
        //}

       

       
    }
}
