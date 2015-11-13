using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NFI.Startup))]
namespace NFI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
