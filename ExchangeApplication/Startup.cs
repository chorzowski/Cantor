using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExchangeApplication.Startup))]
namespace ExchangeApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
