using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Link2Web.Startup))]
namespace Link2Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
