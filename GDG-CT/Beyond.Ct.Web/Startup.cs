using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Beyond.Ct.Web.Startup))]
namespace Beyond.Ct.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
