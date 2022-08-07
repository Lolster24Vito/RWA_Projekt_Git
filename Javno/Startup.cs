using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Javno.Startup))]
namespace Javno
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
