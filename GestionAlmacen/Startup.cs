using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ALC.IES.GestionAlmacen.Startup))]
namespace ALC.IES.GestionAlmacen {
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
