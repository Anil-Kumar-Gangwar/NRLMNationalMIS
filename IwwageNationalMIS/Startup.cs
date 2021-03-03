using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IwwageNationalMIS.Startup))]
namespace IwwageNationalMIS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
