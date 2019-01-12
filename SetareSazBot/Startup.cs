using Microsoft.Owin;
using Owin;
using SetareSazBot;

[assembly: OwinStartup(typeof(Startup))]

namespace SetareSazBot
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
