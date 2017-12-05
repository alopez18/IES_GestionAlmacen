using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALC.IES.GestionAlmacen.Models {
    public class AlmacenGestionModel {

        public List<Models.PCAModel> PCAs { get; set; }
        public List<TerminalGestionModel> Terminales { get; set; }

        public AlmacenGestionModel() {

        }


        public void CargaJson() {
            this.PCAs = cls.DatosUtils.GetPCAs();
            this.Terminales = cls.DatosUtils.GetTerminales();
        }


        internal void CargaRandom() {
            Random rPCA = new Random(DateTime.Now.Millisecond);
            Random rUsuarios = new Random(rPCA.Next(100000, 999999));
            Random rTerminales = new Random(rPCA.Next(100000, 999999));

            this.PCAs = new List<PCAModel>();
            int l = rPCA.Next(3, 15);
            for (int i = 0; i < l; i++) {
                PCAModel modelPcaAux = new PCAModel() {
                    Alarma = (rPCA.Next() % 2 == 0),
                    Almacen = rPCA.Next(1, 15),
                    Escandallo = rPCA.Next(100000, 999999),
                    FechaEntrega = new DateTime(2017, 11, 10),
                    Id = rPCA.Next(1000, 9999),
                    PickingsActual = rPCA.Next(0, 5),
                    PickingsTotal = rPCA.Next(6, 20),
                    Proveedor = "CONVERSE NETHERLANDS BV BELGICA",
                    Usuarios = new List<string>(),
                    CBLoMosca = (rPCA.Next() % 2 == 0)
                };

                Random rUserPCA = new Random();
                if (!(rPCA.Next() % 5 == 0)) {//Así decidimos si tendrá usuarios
                    int cuantos = rPCA.Next(0, 3);
                    for (int i2 = 0; i2 < cuantos; i2++) {
                        modelPcaAux.Usuarios.Add(UsuariosModel._Usuarios[rUserPCA.Next(0, UsuariosModel._Usuarios.Count)]);
                    }
                }
                this.PCAs.Add(modelPcaAux);
            }

            /*
             * Así conseguimos los usuarios que están en algún pca.
             */
            this.Terminales = new List<TerminalGestionModel>();
            List<String> UsuariosAsignados = new List<string>();
            foreach (var item in this.PCAs) {
                foreach (var item2 in item.Usuarios) {
                    if (!UsuariosAsignados.Contains(item2)) {
                        UsuariosAsignados.Add(item2);
                    }
                }
            }

            for (int i = 0; i < 15; i++) {
                int countPca = rTerminales.Next(0, l);
                TerminalGestionModel tModelAux = new TerminalGestionModel() {
                    Id = i + 1
                };

                if (UsuariosAsignados.Count > i) {
                    tModelAux.NombreUsuario = UsuariosAsignados[i];
                    List<PCAModel> pcasUsuario = this.PCAs.Where(m => m.Usuarios.Contains(UsuariosAsignados[i])).ToList();
                    tModelAux.PCAs = pcasUsuario;
                }


                this.Terminales.Add(tModelAux);
            }





        }
    }//Class Finish
}//Namespace Finish