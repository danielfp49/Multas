using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Multas.Startup))]
namespace Multas
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
