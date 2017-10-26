using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Routing;

[assembly: OwinStartup(typeof(polite.Startup))]

namespace polite
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
