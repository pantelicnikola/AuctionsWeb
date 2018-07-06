using AuctionsWeb.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuctionsWeb.Startup))]
namespace AuctionsWeb
{
    public partial class Startup
    {
        public async void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
            await ConfigureRole();
        }
    }
}
