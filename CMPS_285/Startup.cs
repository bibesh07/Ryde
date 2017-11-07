using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMPS_285.Startup))]
namespace CMPS_285
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
