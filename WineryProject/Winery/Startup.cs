using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Winery.Startup))]
namespace Winery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
