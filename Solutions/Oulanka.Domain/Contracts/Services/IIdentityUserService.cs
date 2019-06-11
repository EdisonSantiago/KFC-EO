using System;
using System.Linq;
using System.Linq.Expressions;
using Oulanka.Domain.Models.Identity;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IIdentityUserService 
    {
        void Dispose();
        ActionConfirmation SaveOrUpdate(IdentityUser user);
        ActionConfirmation Delete(IdentityUser user);
        ActionConfirmation Delete(string userId);
        IdentityUser GetUserFromProvider(string loginProvider, string providerKey);
        IQueryable<IdentityUser> GetUsers();
        IdentityUser GetUser(Expression<Func<IdentityUser, bool>> filter);
    }
}