using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALC.IES.GestionAlmacen.Models {
    public class BackendModel {
        public List<cls.PCA> PCAs { get; set; }
        public List<cls.TerminalGestion> Terminales { get; set; }

        public List<String> UsersLibres { get; set; }



        public BackendModel() {
            this.Terminales = cls.DatosUtils.GetTerminales();
            this.PCAs = cls.DatosUtils.GetPCAs();

            this.UsersLibres = new List<string>();
            foreach (String user in Models.UsuariosModel._Usuarios) {
                if (!this.Terminales.Any(m=>m.NombreUsuario==user)) {
                    this.UsersLibres.Add(user);
                }
            }
        }


    }//Class Finish
}//Namespace Finish