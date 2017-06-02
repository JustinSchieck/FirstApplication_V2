using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FirstApplication_V2.Startup))]
namespace FirstApplication_V2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
