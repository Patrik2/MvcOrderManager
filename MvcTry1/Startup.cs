using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcTry1.Startup))]
namespace MvcTry1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
