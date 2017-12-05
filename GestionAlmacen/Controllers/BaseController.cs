using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ALC.IES.GestionAlmacen.Controllers {
    public class BaseController : Controller {
        public static string RenderViewToString(string controllerName, string viewName, object viewModel) {
            using (var writer = new StringWriter()) {
                var routeData = new RouteData();
                routeData.Values.Add("controller", controllerName);
                var fakeControllerContext = new ControllerContext(new HttpContextWrapper(new HttpContext(new HttpRequest(null, "http://google.com", null), new HttpResponse(null))), routeData, new BaseController());
                var razorViewEngine = new RazorViewEngine();
                var razorViewResult = razorViewEngine.FindView(fakeControllerContext, viewName, "", false);

                var viewContext = new ViewContext(fakeControllerContext, razorViewResult.View, new ViewDataDictionary(viewModel), new TempDataDictionary(), writer);
                razorViewResult.View.Render(viewContext, writer);
                return writer.ToString();
            }
        }

        public static string RenderViewAreaToString(string controllerName, string viewName, string areaName, object viewModel, RequestContext rctx) {
            try {
                using (var writer = new StringWriter()) {
                    var routeData = new RouteData();
                    routeData.Values.Add("controller", controllerName);
                    routeData.Values.Add("Area", areaName);
                    routeData.DataTokens["area"] = areaName;

                    var fakeControllerContext = new ControllerContext(rctx, new BaseController());
                    //new ControllerContext(new HttpContextWrapper(new HttpContext(new HttpRequest(null, "http://google.com", null), new HttpResponse(null))), routeData, new FakeController());
                    fakeControllerContext.RouteData = routeData;
                    var razorViewEngine = new RazorViewEngine();
                    var razorViewResult = razorViewEngine.FindView(fakeControllerContext, viewName, "", false);
                    var viewContext = new ViewContext(fakeControllerContext, razorViewResult.View, new ViewDataDictionary(viewModel), new TempDataDictionary(), writer);

                    razorViewResult.View.Render(viewContext, writer);
                    return writer.GetStringBuilder().ToString();
                    //use example
                    //String renderedHTML = RenderViewToString("Email", "MyHTMLView", myModel );
                    //where file MyHTMLView.cstml is stored in Views/Email/MyHTMLView.cshtml. Email is a fake controller name.
                }
            } catch (Exception ex) {
                //do your exception handling here
                throw ex;
            }
        }

    }//Class Finish
}//Namespace Finish