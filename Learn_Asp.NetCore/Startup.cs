using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Learn_Asp.NetCore.Startup))]
namespace Learn_Asp.NetCore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
