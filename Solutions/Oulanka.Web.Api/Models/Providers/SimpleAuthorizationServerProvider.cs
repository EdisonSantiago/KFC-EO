using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models.Identity;

namespace Oulanka.Web.Api.Models.Providers
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

            var userService = ServiceLocator.Current.GetInstance<IUserAccountService>();

            var user = userService.GetUser(context.UserName);
            if (user == null)
            {
                context.SetError("invalid_grant", "Usuario no existe.");
                return;
            }

            if (user.LocalPassword == context.Password)
            {
                context.SetError("invalid_grant", "Contraseña incorrecta.");
                return;
            }


            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));
            identity.AddClaim(new Claim("username",user.NombreUsuario));
            identity.AddClaim(new Claim("userId",user.Id.ToString()));

            context.Validated(identity);

        }
    }
}