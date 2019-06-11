using System.Linq;
using Oulanka.Domain.Models.Identity;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IIdentityRoleService
    {
        IdentityRole GetRoleByName(string roleName);
        IdentityRole GetRoleById(string roleId);
        ActionConfirmation SaveOrUpdate(IdentityRole role);
        ActionConfirmation Delete(IdentityRole role);
        IQueryable<IdentityRole> GetRoles();
    }
}