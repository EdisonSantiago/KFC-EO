using System;
using System.Linq;
using Oulanka.Domain;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models.Identity;

namespace Oulanka.Services.Identity
{
    public class IdentityRoleService : IIdentityRoleService
    {
        private readonly IIdentityRoleRepository _roleRepository;

        public IdentityRoleService(IIdentityRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IdentityRole GetRoleByName(string roleName)
        {
            return _roleRepository.GetByName(roleName);
        }

        public IdentityRole GetRoleById(string roleId)
        {
            return _roleRepository.GetById(roleId);
        }

        public ActionConfirmation SaveOrUpdate(IdentityRole role)
        {
            if (role.IsValid())
            {
                try
                {
                    _roleRepository.SaveOrUpdate(role);
                    _roleRepository.DbContext.CommitChanges();

                    return ActionConfirmation.CreateSuccess("role saved");
                }
                catch (Exception exception)
                {
                    return ActionConfirmation.CreateFailure("error > " + exception.Message);
                }
            }
            else
            {
                return ActionConfirmation.CreateFailure("role is not valid");
            }
        }

        public ActionConfirmation Delete(IdentityRole role)
        {
            try
            {
                _roleRepository.Delete(role);
                _roleRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("role deleted");
            }
            catch (Exception exception)
            {
                return ActionConfirmation.CreateFailure("error > " + exception.Message);
            }

        }

        public IQueryable<IdentityRole> GetRoles()
        {
            return _roleRepository.GetAll().AsQueryable();
        }
    }
}