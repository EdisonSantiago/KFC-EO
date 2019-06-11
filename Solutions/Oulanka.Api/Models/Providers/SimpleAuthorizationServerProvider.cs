using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models.Identity;
using Oulanka.Web.Core;

namespace Oulanka.Api.Models.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

           context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var authService = ServiceLocator.Current.GetInstance<IUserAccountService>();

            var encPassword = Crypto.EncryptString(context.Password);
            var oulUser = authService.Login(context.UserName, encPassword);

            if (oulUser == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);

        }
    }
}