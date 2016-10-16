using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LexiconGarage.Startup))]
namespace LexiconGarage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
