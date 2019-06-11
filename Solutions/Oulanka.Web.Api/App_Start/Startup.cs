using System;
using Microsoft.Owin;
using Oulanka.Web.Api;
using Owin;
using System.Web;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Oulanka.Web.Api.Models.Providers;

[assembly: OwinStartup(typeof(Startup))]
namespace Oulanka.Web.Api
{
    public class Startup
    {
        private static string _applicationPath = string.Empty;
        private static string _contentRootPath = string.Empty;


        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            ConfigureOAuth(app);
            app.UseCors(CorsOptions.AllowAll);

            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
