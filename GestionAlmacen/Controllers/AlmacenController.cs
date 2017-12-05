using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALC.IES.GestionAlmacen.Controllers {
    public class AlmacenController : BaseController {
        // GET: Almacen
        public ActionResult Index() {
            Models.AlmacenGestionModel model = new Models.AlmacenGestionModel();


            // model.CargaRandom();
            model.CargaJson();

            return View(model);
        }
    }//Class Finish
}//namespace Finish