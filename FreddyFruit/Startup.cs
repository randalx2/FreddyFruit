using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FreddyFruit.Startup))]
namespace FreddyFruit
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
