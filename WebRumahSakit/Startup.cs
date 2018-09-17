using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebRumahSakit.Startup))]
namespace WebRumahSakit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
