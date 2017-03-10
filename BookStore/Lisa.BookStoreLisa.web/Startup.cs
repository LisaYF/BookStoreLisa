using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lisa.BookStoreLisa.web.Startup))]
namespace Lisa.BookStoreLisa.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
