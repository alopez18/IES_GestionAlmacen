using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ALC.IES.GestionAlmacen.Startup))]
namespace ALC.IES.GestionAlmacen {
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();

        }


    }//Class Finish
}//Namespace Finish
