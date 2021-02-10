using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PretrazivacCasopisa.Startup))]
namespace PretrazivacCasopisa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
