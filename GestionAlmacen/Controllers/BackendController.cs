using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALC.IES.GestionAlmacen.Controllers {
    public class BackendController : BaseController {
        // GET: Backend
        public ActionResult Index() {

            return View();
        }


        public JsonResult ActivarTerminal(int terminal, String usuario) {
            DTO.DtoAjaxReturn res = new DTO.DtoAjaxReturn();

            List<Models.TerminalGestionModel> models = cls.DatosUtils.GetTerminales();
            models[terminal - 1].NombreUsuario = usuario;
            cls.DatosUtils.SetTerminales(models);


            Hubs.TerminalesHub hub = new Hubs.TerminalesHub();
            hub.ActivarTerminal(terminal, usuario);
            res.Success = true;
            return Json(res);
        }


        public JsonResult SetPCA2Terminal(int pca, int terminal) {
            DTO.DtoAjaxReturn res = new DTO.DtoAjaxReturn();
            List<Models.PCAModel> pcaModels = cls.DatosUtils.GetPCAs();
            Models.PCAModel pcaModel = pcaModels.FirstOrDefault(m => m.Id == pca);

            List<Models.TerminalGestionModel> models = cls.DatosUtils.GetTerminales();
            models[terminal - 1].PCAs.Add(pcaModel);
            cls.DatosUtils.SetTerminales(models);

            pcaModel.Usuarios.Add(models[terminal - 1].NombreUsuario);

            Hubs.TerminalesHub hub = new Hubs.TerminalesHub();

            String html = RenderViewToString("Almacen", "_pcaEnTerminal", pcaModel);
            hub.SetPCA2Terminal(pca, terminal, html);
            res.Success = true;
            return Json(res);
        }



        public JsonResult RestaurarDatosBase() {
            DTO.DtoAjaxReturn res = new DTO.DtoAjaxReturn(true);

            List<Models.PCAModel> pcaModels = new List<Models.PCAModel>();
            Models.PCAModel model1 = new Models.PCAModel() {
                Alarma = false,
                Almacen = 2,
                CBLoMosca = true,
                Escandallo = 486321,
                FechaEntrega = new DateTime(2017, 11, 14),
                Id = 145789,
                PickingsActual = 2,
                PickingsTotal = 14,
                Proveedor = "CONVERSE NETHERLANDS BV BELGICA",
                Usuarios = new List<string>() {
                              Models.UsuariosModel._Usuarios[4]
                          }
            };
            pcaModels.Add(model1);

            Models.PCAModel model2 = new Models.PCAModel() {
                Alarma = false,
                Almacen = 2,
                CBLoMosca = true,
                Escandallo = 458963,
                FechaEntrega = new DateTime(2017, 12, 1),
                Id = 784563,
                PickingsActual = 5,
                PickingsTotal = 10,
                Proveedor = "CONVERSE NETHERLANDS BV BELGICA",
                Usuarios = new List<string>() {
                              Models.UsuariosModel._Usuarios[1]
                          }
            };
            pcaModels.Add(model2);

            Models.PCAModel model3 = new Models.PCAModel() {
                Alarma = true,
                Almacen = 5,
                CBLoMosca = true,
                Escandallo = 985762,
                FechaEntrega = new DateTime(2017, 11, 8),
                Id = 235487,
                PickingsActual = 0,
                PickingsTotal = 12,
                Proveedor = "CONVERSE NETHERLANDS BV BELGICA",
                Usuarios = new List<string>()
            };
            pcaModels.Add(model3);

            Models.PCAModel model4 = new Models.PCAModel() {
                Alarma = false,
                Almacen = 3,
                CBLoMosca = false,
                Escandallo = 568974,
                FechaEntrega = new DateTime(2017, 11, 3),
                Id = 639854,
                PickingsActual = 3,
                PickingsTotal = 7,
                Proveedor = "CONVERSE NETHERLANDS BV BELGICA",
                Usuarios = new List<string>()
            };
            pcaModels.Add(model4);

            Models.PCAModel model5 = new Models.PCAModel() {
                Alarma = true,
                Almacen = 3,
                CBLoMosca = false,
                Escandallo = 589563,
                FechaEntrega = new DateTime(2017, 11, 23),
                Id = 639854,
                PickingsActual = 6,
                PickingsTotal = 9,
                Proveedor = "CONVERSE NETHERLANDS BV BELGICA",
                Usuarios = new List<string>() {
                     Models.UsuariosModel._Usuarios[5]
                }
            };
            pcaModels.Add(model5);
            cls.DatosUtils.SetPCAs(pcaModels);



            List<Models.TerminalGestionModel> tModels = new List<Models.TerminalGestionModel>();
            for (int i = 1; i <= 15; i++) {
                Models.TerminalGestionModel tModel = new Models.TerminalGestionModel() {
                    Id = i
                };
                tModels.Add(tModel);
            }
            tModels[3].NombreUsuario = Models.UsuariosModel._Usuarios[5];
            tModels[8].NombreUsuario = Models.UsuariosModel._Usuarios[1];
            tModels[1].NombreUsuario = Models.UsuariosModel._Usuarios[4];

            tModels[3].PCAs = pcaModels.Where(m => m.Usuarios.Contains(Models.UsuariosModel._Usuarios[5])).ToList();
            tModels[8].PCAs = pcaModels.Where(m => m.Usuarios.Contains(Models.UsuariosModel._Usuarios[1])).ToList();
            tModels[1].PCAs = pcaModels.Where(m => m.Usuarios.Contains(Models.UsuariosModel._Usuarios[4])).ToList();

            cls.DatosUtils.SetTerminales(tModels);

            return Json(res, JsonRequestBehavior.AllowGet);
        }

    }//Class Finish
}//Namespace Finish