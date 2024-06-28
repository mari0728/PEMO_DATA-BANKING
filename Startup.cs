using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PEMO_DATA_BANKING.Startup))]
namespace PEMO_DATA_BANKING
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
