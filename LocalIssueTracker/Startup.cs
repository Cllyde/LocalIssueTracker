using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LocalIssueTracker.Startup))]
namespace LocalIssueTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
