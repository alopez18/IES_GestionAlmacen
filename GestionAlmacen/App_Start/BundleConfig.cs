using System.Web;
using System.Web.Optimization;

namespace ALC.IES.GestionAlmacen {
    public class BundleConfig {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new StyleBundle("~/bundles/simpleLineIcons").Include(
                "~/bower_components/simple-line-icons/css/simple-line-icons.css", new CssRewriteUrlTransform()
            ));
            bundles.Add(new StyleBundle("~/bundles/fontawesome").Include(
                "~/bower_components/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()
            ));
            bundles.Add(new StyleBundle("~/bundles/animatecss").Include(
                "~/bower_components/animate.css/animate.min.css"
            ));

            bundles.Add(new StyleBundle("~/bundles/whirl").Include(
                "~/bower_components/whirl/dist/whirl.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap").Include(
                "~/Content/bootstrap.min.css"
            ));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"
            ));

            bundles.Add(new ScriptBundle("~/bundles/matchMedia").Include(
                "~/Vendor/matchMedia/matchMedia.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                            "~/Scripts/jquery-{version}.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/storage").Include(
                "~/Vendor/jQuery-Storage-API/jquery.storageapi.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryEasing").Include(
                "~/Scripts/jquery.easing.1.3.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/animo").Include(
                "~/bower_components/animo/animo.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/slimscroll").Include(
                "~/bower_components/jquery-slimscroll/jquery.slimscroll.min.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/screenfull").Include(
                "~/bower_components/screenfull/dist/screenfull.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/appinit").Include(
                "~/Scripts/app/app.init.js",
                //Modulos
                "~/Scripts/app/modules/bootstrap-start.js",
                "~/Scripts/app/modules/toggle-state.js",
                "~/Scripts/app/modules/navbar-search.js",
                "~/Scripts/app/modules/fullscreen.js",
                "~/Scripts/app/modules/slimscroll.js"
            ));
        }
    }
}
