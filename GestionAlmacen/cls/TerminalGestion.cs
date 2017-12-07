using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALC.IES.GestionAlmacen.cls {
    public class TerminalGestion {
        public int Id { get; set; }
        public String NombreUsuario { get; set; }
        public List<cls.PCA> PCAs { get; set; }

        public TerminalGestion() {
            this.PCAs = new List<cls.PCA>();
        }
    }//Class Finish
}//Namespace Finish