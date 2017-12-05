using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALC.IES.GestionAlmacen.DTO {
    public class DtoAjaxReturn {
        /// <summary>
        /// Contiene si la petición ha ido bien o no.
        /// </summary>
        public Boolean Success {
            get;
            set;
        }
        /// <summary>
        /// Contenido del resultado de la aplicación.
        /// </summary>
        public Object Data {
            get;
            set;
        }
        /// <summary>
        /// En caso de error por dependencia en BBDD, informar aquí.
        /// </summary>
        public Boolean Foreign {
            get;
            set;
        }

        public String ErrorTecnico {
            get;
            set;
        }

        public String Msg {
            get;
            set;
        }

        public String WarningMsg {
            get;
            set;
        }

        //public Exception EXC { get; set; }

        #region Constructores
        public DtoAjaxReturn(Boolean bSuccess = false) {
            Success = bSuccess;
            this.Foreign = false;
        }
        #endregion
    }//Class Finish
}//Namespace Finish