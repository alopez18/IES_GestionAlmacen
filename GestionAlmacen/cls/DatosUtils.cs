using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using ALC.IES.GestionAlmacen.Models;

namespace ALC.IES.GestionAlmacen.cls {
    public class DatosUtils {
        private static String _pcasFileName = "pcas.json";
        private static String _terminalesFileName = "terminales.json";

        public DatosUtils() {

        }




        public static List<Models.PCAModel> GetPCAs() {
            String path = HttpContext.Current.Server.MapPath(String.Format("~/App_Data/{0}", _pcasFileName));
            String ser = File.ReadAllText(path, Encoding.UTF8);
            List<Models.PCAModel> res = JsonConvert.DeserializeObject<List<Models.PCAModel>>(ser);
            return res;
        }


        public static void SetPCAs(List<Models.PCAModel> model) {
            String path = HttpContext.Current.Server.MapPath(String.Format("~/App_Data/{0}", _pcasFileName));
            String ser = JsonConvert.SerializeObject(model, Formatting.Indented);
            File.WriteAllText(path, ser, Encoding.UTF8);
        }

        internal static void SetTerminales(List<TerminalGestionModel> tModels) {
            String path = HttpContext.Current.Server.MapPath(String.Format("~/App_Data/{0}", _terminalesFileName));
            String ser = JsonConvert.SerializeObject(tModels, Formatting.Indented);
            File.WriteAllText(path, ser, Encoding.UTF8);
        }

        public static List<TerminalGestionModel> GetTerminales() {
            String path = HttpContext.Current.Server.MapPath(String.Format("~/App_Data/{0}", _terminalesFileName));
            String ser = File.ReadAllText(path, Encoding.UTF8);
            List<TerminalGestionModel> res = JsonConvert.DeserializeObject<List<TerminalGestionModel>>(ser);
            return res;
        }
    }//Class Finish
}//Namespace Finish