using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALC.IES.GestionAlmacen.Controllers {
    public class BackendController : BaseController {
        // GET: Backend
        public ActionResult Index() {
            Models.BackendModel model = new Models.BackendModel();
            return View(model);
        }

        public JsonResult DesactivarTerminal(int id) {
            DTO.DtoAjaxReturn res = new DTO.DtoAjaxReturn();

            //Actualizamos los terminales
            List<cls.TerminalGestion> models = cls.DatosUtils.GetTerminales();
            String sAuxUsuario = models[id - 1].NombreUsuario;
            models[id - 1].NombreUsuario = null;
            models[id - 1].PCAs.Clear();
            cls.DatosUtils.SetTerminales(models);

            //Actualizamos los pcas
            List<cls.PCA> pcas = cls.DatosUtils.GetPCAs();
            foreach (cls.PCA pca in pcas) {
                pca.Usuarios.Remove(sAuxUsuario);
            }
            cls.DatosUtils.SetPCAs(pcas);

            //Informamos a los hubs del cambio
            Hubs.TerminalesHub hub = new Hubs.TerminalesHub();
            hub.DesactivarTerminal(id);

            //Devolvemos resultado
            res.Success = true;
            return Json(res);
        }

        public JsonResult ActivarTerminal(int terminal, String usuario) {
            DTO.DtoAjaxReturn res = new DTO.DtoAjaxReturn();

            List<cls.TerminalGestion> models = cls.DatosUtils.GetTerminales();
            models[terminal - 1].NombreUsuario = usuario;
            cls.DatosUtils.SetTerminales(models);

            //Si el terminal tenia algún pca asignado, al activar el terminal lo activamos en el pca.
            if (models[terminal - 1].PCAs != null && models[terminal - 1].PCAs.Count > 0) {
                List<cls.PCA> pcas = cls.DatosUtils.GetPCAs();
                foreach (var pca in pcas) {
                    foreach (var pcaT in models[terminal - 1].PCAs) {
                        if (pca.Id == pcaT.Id) {
                            pca.Usuarios.Add(models[terminal - 1].NombreUsuario);
                        }
                    }
                }
                cls.DatosUtils.SetPCAs(pcas);
            }

            Hubs.TerminalesHub hub = new Hubs.TerminalesHub();
            hub.ActivarTerminal(terminal, usuario);
            res.Success = true;
            return Json(res);
        }


        public JsonResult SetPCA2Terminal(int pca, int terminal) {
            DTO.DtoAjaxReturn res = new DTO.DtoAjaxReturn();
            List<cls.PCA> pcaModels = cls.DatosUtils.GetPCAs();
            cls.PCA pcaModel = pcaModels.FirstOrDefault(m => m.Id == pca);

            List<cls.TerminalGestion> models = cls.DatosUtils.GetTerminales();
            models[terminal - 1].PCAs.Add(pcaModel);
            cls.DatosUtils.SetTerminales(models);

            pcaModel.Usuarios.Add(models[terminal - 1].NombreUsuario);
            cls.DatosUtils.SetPCAs(pcaModels);

            Hubs.TerminalesHub hub = new Hubs.TerminalesHub();

            String html = RenderViewToString("Almacen", "_pcaEnTerminal", pcaModel);
            hub.SetPCA2Terminal(pca, terminal, html);
            res.Success = true;
            return Json(res);
        }

        public JsonResult MovePCA2Terminal(int pca, int terminalOld, int terminalNew) {
            DTO.DtoAjaxReturn res = new DTO.DtoAjaxReturn();
            List<cls.PCA> pcaModels = cls.DatosUtils.GetPCAs();
            cls.PCA pcaModel = pcaModels.FirstOrDefault(m => m.Id == pca);

            List<cls.TerminalGestion> models = cls.DatosUtils.GetTerminales();
            models[terminalNew - 1].PCAs.Add(pcaModel);

            int index = models[terminalOld - 1].PCAs.IndexOf(models[terminalOld - 1].PCAs.First(m => m.Id == pcaModel.Id));

            models[terminalOld - 1].PCAs.RemoveAt(index);
            cls.DatosUtils.SetTerminales(models);

            pcaModel.Usuarios.Add(models[terminalNew - 1].NombreUsuario);
            cls.DatosUtils.SetPCAs(pcaModels);

            Hubs.TerminalesHub hub = new Hubs.TerminalesHub();

            String html = RenderViewToString("Almacen", "_pcaEnTerminal", pcaModel);
            hub.MovePCA(pca, terminalOld, terminalNew, html);
            res.Success = true;
            return Json(res);
        }


        public JsonResult PickItemLinea(int id) {
            DTO.DtoAjaxReturn res = new DTO.DtoAjaxReturn();
            int idPCA;
            if (cls.Linea.PickItem(id, out idPCA)) {
                Hubs.TerminalesHub hub = new Hubs.TerminalesHub();
                hub.RefreshPCAinTerminales(idPCA);
                res.Success = true;
            }
            return Json(res);
        }

        public JsonResult RestaurarDatosBase() {
            DTO.DtoAjaxReturn res = new DTO.DtoAjaxReturn(true);

            List<cls.PCA> pcaModels = new List<cls.PCA>();


            cls.PCA model1 = new cls.PCA() {
                Alarma = false,
                Almacen = 2,
                CBLoMosca = true,
                Escandallo = 486321,
                FechaEntrega = new DateTime(2017, 11, 14),
                Id = 145789,
                Proveedor = "CONVERSE NETHERLANDS BV BELGICA",
                Usuarios = new List<string>() {
                              Models.UsuariosModel._Usuarios[4]
                          },
                Pickings = new List<cls.Picking>()
            };

            model1.Pickings.Add(new cls.Picking() {
                new cls.Linea(){ Id=153211, Cantidad=2, Producto="Zapatillas Nike"},
                new cls.Linea(){ Id=153212,Cantidad=1, Producto="Sudadera ASICS"},
                new cls.Linea(){ Id=153213,Cantidad=3, Producto="Calcetines adidas"}
            });
            model1.Pickings.Add(new cls.Picking() {
                new cls.Linea(){Id=153214, Cantidad=2, Producto="Zapatillas Nike"},
                new cls.Linea(){Id=153215, Cantidad=1, Producto="Sudadera ASICS"},
                new cls.Linea(){Id=153216, Cantidad=3, Producto="Calcetines adidas"}
            });
            model1.Pickings.Add(new cls.Picking() {
                new cls.Linea(){Id=153217, Cantidad=2, Producto="Zapatillas Nike"},
                new cls.Linea(){Id=153218, Cantidad=1, Producto="Sudadera ASICS"},
                new cls.Linea(){Id=153219, Cantidad=3, Producto="Calcetines adidas"}
            });

            pcaModels.Add(model1);

            cls.PCA model2 = new cls.PCA() {
                Alarma = false,
                Almacen = 2,
                CBLoMosca = true,
                Escandallo = 458963,
                FechaEntrega = new DateTime(2017, 12, 1),
                Id = 784563,
                Proveedor = "CONVERSE NETHERLANDS BV BELGICA",
                Usuarios = new List<string>() {
                              Models.UsuariosModel._Usuarios[1]
                          },
                Pickings = new List<cls.Picking>()
            };
            model2.Pickings.Add(new cls.Picking() {
                new cls.Linea(){ Id=153220, Cantidad=2, Producto="Zapatillas Padel ASICS"},
                new cls.Linea(){ Id=153221,  Cantidad=1, Producto="Sudadera NIKE"},
                new cls.Linea(){ Id=153222, Cantidad=3, Producto="Calcetines adidas"},
                new cls.Linea(){ Id=153223, Cantidad=1, Producto="Gorra Under Armour"}
            });

            pcaModels.Add(model2);

            cls.PCA model3 = new cls.PCA() {
                Alarma = true,
                Almacen = 5,
                CBLoMosca = true,
                Escandallo = 985762,
                FechaEntrega = new DateTime(2017, 11, 8),
                Id = 235487,
                Proveedor = "CONVERSE NETHERLANDS BV BELGICA",
                Usuarios = new List<string>(),
                Pickings = new List<cls.Picking>()
            };
            model3.Pickings.Add(new cls.Picking() {
                new cls.Linea(){ Id=153230, Cantidad=2, Producto="Zapatillas Padel ASICS"},
                new cls.Linea(){ Id=153231, Cantidad=1, Producto="Sudadera NIKE"},
                new cls.Linea(){ Id=153232, Cantidad=3, Producto="Calcetines adidas"},
                new cls.Linea(){ Id=153233, Cantidad=1, Producto="Gorra Under Armour"}
            });
            pcaModels.Add(model3);

            cls.PCA model4 = new cls.PCA() {
                Alarma = false,
                Almacen = 3,
                CBLoMosca = false,
                Escandallo = 568974,
                FechaEntrega = new DateTime(2017, 11, 3),
                Id = 639854,
                Proveedor = "CONVERSE NETHERLANDS BV BELGICA",
                Usuarios = new List<string>(),
                Pickings = new List<cls.Picking>()
            };
            model4.Pickings.Add(new cls.Picking() {
                new cls.Linea(){ Id=153240, Cantidad=2, Producto="Zapatillas Padel ASICS"},
                new cls.Linea(){ Id=153241, Cantidad=1, Producto="Sudadera NIKE"},
                new cls.Linea(){ Id=153242, Cantidad=3, Producto="Calcetines adidas"},
                new cls.Linea(){ Id=153243, Cantidad=1, Producto="Gorra Under Armour"}
            });
            pcaModels.Add(model4);

            cls.PCA model5 = new cls.PCA() {
                Alarma = true,
                Almacen = 3,
                CBLoMosca = false,
                Escandallo = 589563,
                FechaEntrega = new DateTime(2017, 11, 23),
                Id = 639854,
                Proveedor = "CONVERSE NETHERLANDS BV BELGICA",
                Usuarios = new List<string>() {
                     Models.UsuariosModel._Usuarios[5]
                },
                Pickings = new List<cls.Picking>()
            };
            model5.Pickings.Add(new cls.Picking() {
                new cls.Linea(){ Id=153244,  Cantidad=2, Producto="Zapatillas Padel ASICS"},
                new cls.Linea(){ Id=153245, Cantidad=1, Producto="Camiseta NIKE"},
                new cls.Linea(){ Id=153246,  Cantidad=3, Producto="Calcetines Trango"}
            });
            pcaModels.Add(model5);
            cls.DatosUtils.SetPCAs(pcaModels);

            /*
             * Terminales
             */

            List<cls.TerminalGestion> tModels = new List<cls.TerminalGestion>();
            for (int i = 1; i <= 15; i++) {
                cls.TerminalGestion tModel = new cls.TerminalGestion() {
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