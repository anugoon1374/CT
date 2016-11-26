using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GDG_Ct.Startup))]
namespace GDG_Ct
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
