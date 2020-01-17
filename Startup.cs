using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MJC_Blogs.Startup))]
namespace MJC_Blogs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
