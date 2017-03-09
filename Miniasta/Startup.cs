using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Miniasta.Startup))]
namespace Miniasta
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
