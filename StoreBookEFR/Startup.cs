using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StoreBookEFR.Startup))]
namespace StoreBookEFR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
