using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gighub1.Startup))]
namespace Gighub1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
