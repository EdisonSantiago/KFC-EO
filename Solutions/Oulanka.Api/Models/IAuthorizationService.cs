using System.Security.Principal;
using System.Threading.Tasks;

namespace Oulanka.Api.Models
{
    public interface IAuthorizationService
    {
        Task<bool> AuthorizeAsync(IPrincipal user);
    }
}