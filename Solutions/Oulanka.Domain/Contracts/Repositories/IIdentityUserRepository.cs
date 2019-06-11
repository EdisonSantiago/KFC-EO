using System;
using System.Linq.Expressions;
using Oulanka.Domain.Models.Identity;
using SharpArch.Domain.PersistenceSupport;

namespace Oulanka.Domain.Contracts.Repositories
{
    public interface IIdentityUserRepository : IRepository<IdentityUser>
    {
        IdentityUser GetUser(Expression<Func<IdentityUser, bool>> filter);
        IdentityUser GetById(string userId);
        IdentityUser GetUserFromLogin(string loginProvider, string providerKey);
    }
}