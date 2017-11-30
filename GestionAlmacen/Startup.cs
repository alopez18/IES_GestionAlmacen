using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GestionAlmacen.Startup))]
namespace GestionAlmacen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
