using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALC.IES.GestionAlmacen.Models {
    public class BackendModel {
        public List<Models.PCAModel> PCAs { get; set; }
        public List<Models.TerminalGestionModel> Terminales { get; set; }

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