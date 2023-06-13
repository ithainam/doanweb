using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebsiteBanCoffee.Startup))]
namespace WebsiteBanCoffee
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
