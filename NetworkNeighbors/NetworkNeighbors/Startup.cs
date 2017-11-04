using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NetworkNeighbors.Startup))]
namespace NetworkNeighbors
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
