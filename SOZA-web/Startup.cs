using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SOZA_web.Startup))]
namespace SOZA_web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
