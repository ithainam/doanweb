using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(web3.Startup))]
namespace web3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
