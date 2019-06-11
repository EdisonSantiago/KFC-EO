using Microsoft.Owin;
using Oulanka.Web.Mvc;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Oulanka.Web.Mvc
{
    public class Startup
    {
        private static string _applicationPath = string.Empty;
        private static string _contentRootPath = string.Empty;

        public Startup()
        {
        }

        public void Configuration(IAppBuilder app)
        {
        }
    }
}
