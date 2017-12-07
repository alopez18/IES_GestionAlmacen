using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALC.IES.GestionAlmacen.cls {
    public class PCA {
        public int Id { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int Almacen { get; set; }
        public String Proveedor { get; set; }
        public int Escandallo { get; set; }
        public bool Alarma { get; set; }

        public bool CBLoMosca { get; set; }

        public List<String> Usuarios { get; set; }

        public List<Picking> Pickings { get; set; }

        public int GetCantidad() {
            int res = 0;
            res = this.Pickings.Select(m => m.GetCantidad()).Sum();
            return res;
        }

        public int GetNumPickingsCompletados() {
            int res = 0;
            res = this.Pickings.Count(m => m.Completado());
            return res;
        }

        public int GetRecogidos() {
            int res = 0;
            res = this.Pickings.Select(m => m.GetRecogidos()).Sum();
            return res;
        }


        public int GetNumLineasCompletadas() {
            int res = 0;
            res = this.Pickings.Select(m => m.GetNumLineasCompletadas()).Sum();
            return res;
        }

        public int GetNumLineas() {
            int res = 0;
            res = this.Pickings.Select(m => m.Count).Sum();
            return res;
        }

        public PCA() {
        }
    }//Class Finish

    public class Picking : List<Linea> {


        public Picking() {

        }

        public int GetCantidad() {
            int res = this.Select(m => m.Cantidad).Sum();
            return res;
        }

        public int GetRecogidos() {
            int res = this.Select(m => m.Recogidos).Sum();
            return res;
        }

        public int GetNumLineasCompletadas() {
            //int res = 0;
            //foreach (var item in collection) {

            //}
            //return res;

            int res = this.Count(m => m.Completado());
            return res;
        }

        public Boolean Completado() {
            return (GetNumLineasCompletadas() >= this.GetCantidad());
        }
    }


    public class Linea {
        public String Producto { get; set; }
        public int Cantidad { get; set; }
        public int Recogidos { get; set; }


        public Linea() {

        }

        public Boolean Completado() {
            return (Recogidos >= Cantidad);
        }
    }//Class Finish

}//Namespace Finish