using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NRLMNationalMIS.Startup))]
namespace NRLMNationalMIS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
