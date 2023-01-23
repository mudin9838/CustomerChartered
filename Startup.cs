using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustomerChartered.Startup))]

namespace CustomerChartered
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
