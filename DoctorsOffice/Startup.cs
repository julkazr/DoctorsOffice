using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DoctorsOffice.Startup))]
namespace DoctorsOffice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
