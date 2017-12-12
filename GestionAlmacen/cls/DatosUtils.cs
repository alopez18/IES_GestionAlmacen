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




        public static List<cls.PCA> GetPCAs() {
            String path = HttpContext.Current.Server.MapPath(String.Format("~/App_Data/{0}", _pcasFileName));
            String ser = File.ReadAllText(path, Encoding.UTF8);
            List<cls.PCA> res = JsonConvert.DeserializeObject<List<cls.PCA>>(ser);
            return res;
        }

        internal static PCA GetPCA(int id) {
            List<cls.PCA> pcas = GetPCAs();
            cls.PCA pca = pcas.FirstOrDefault(m => m.Id == id);
            return pca;
        }

        public static void SetPCAs(List<cls.PCA> model) {
            String path = HttpContext.Current.Server.MapPath(String.Format("~/App_Data/{0}", _pcasFileName));
            String ser = JsonConvert.SerializeObject(model, Formatting.Indented);
            File.WriteAllText(path, ser, Encoding.UTF8);
        }

        internal static void SetTerminales(List<cls.TerminalGestion> tModels) {
            String path = HttpContext.Current.Server.MapPath(String.Format("~/App_Data/{0}", _terminalesFileName));
            String ser = JsonConvert.SerializeObject(tModels, Formatting.Indented);
            File.WriteAllText(path, ser, Encoding.UTF8);
        }

        public static List<cls.TerminalGestion> GetTerminales() {
            String path = HttpContext.Current.Server.MapPath(String.Format("~/App_Data/{0}", _terminalesFileName));
            String ser = File.ReadAllText(path, Encoding.UTF8);
            List<cls.TerminalGestion> res = JsonConvert.DeserializeObject<List<cls.TerminalGestion>>(ser);
            return res;
        }
    }//Class Finish
}//Namespace Finish