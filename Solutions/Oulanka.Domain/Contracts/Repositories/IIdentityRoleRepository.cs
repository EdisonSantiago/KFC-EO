using Oulanka.Domain.Models.Identity;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IIdentityRoleRepository : IRepository<IdentityRole>
    {
        IdentityRole GetByName(string roleName);
        IdentityRole GetById(string roleId);
    }
}