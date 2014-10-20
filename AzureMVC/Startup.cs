using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AzureMVC.Startup))]
namespace AzureMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
