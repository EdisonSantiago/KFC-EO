using System.Security.Principal;
using System.Threading.Tasks;

namespace Oulanka.Api.Models.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public async Task<bool> AuthorizeAsync(IPrincipal user)
        {
            return true;
        }
    }
}