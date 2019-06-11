using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models.Identity;

namespace Oulanka.Web.Core.Identity
{
    public class RoleStore<TRole> : IQueryableRoleStore<TRole>, IRoleStore<TRole>, IDisposable where TRole : IdentityRole
    {
        private readonly IIdentityRoleService _roleService;

        public RoleStore(IIdentityRoleService roleService)
        {
            _roleService = roleService;
        }

        public virtual Task<TRole> FindByIdAsync(string roleId)
        {
            return Task.FromResult(_roleService.GetRoleById(roleId) as TRole);
        }

        public virtual Task<TRole> FindByNameAsync(string roleName)
        {
            return Task.FromResult<TRole>(_roleService.GetRoleByName(roleName) as TRole);
        }

        public virtual Task CreateAsync(TRole role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));

            _roleService.SaveOrUpdate(role);

            return Task.FromResult(0);

        }

        public virtual Task DeleteAsync(TRole role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));

            _roleService.Delete(role);
            return Task.FromResult(0);
        }

        public virtual Task UpdateAsync(TRole role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));

            _roleService.SaveOrUpdate(role);

            return Task.FromResult(0);
        }

        public IQueryable<TRole> Roles => _roleService.GetRoles() as IQueryable<TRole>;
        public void Dispose()
        {
            //
        }
    }
}