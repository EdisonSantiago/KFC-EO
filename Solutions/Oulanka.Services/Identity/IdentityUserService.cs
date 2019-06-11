using System;
using System.Linq;
using System.Linq.Expressions;
using Oulanka.Domain;
using Oulanka.Domain.Contracts.Repositories;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models.Identity;

namespace Oulanka.Services.Identity
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly IIdentityUserRepository _userRepository;

        public IdentityUserService(IIdentityUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Dispose()
        {
            // do nothing
        }

        public ActionConfirmation SaveOrUpdate(IdentityUser user)
        {
            if (user.IsValid())
            {
                try
                {
                    _userRepository.SaveOrUpdate(user);
                    _userRepository.DbContext.CommitChanges();

                    return ActionConfirmation.CreateSuccess("user saved");
                }
                catch (Exception exception)
                {
                    return ActionConfirmation.CreateFailure("error > " + exception.Message);
                }
            }
            else
            {
                return ActionConfirmation.CreateFailure("user is not valid");
            }
        }

        public ActionConfirmation Delete(IdentityUser user)
        {
            try
            {
                _userRepository.Delete(user);
                _userRepository.DbContext.CommitChanges();

                return ActionConfirmation.CreateSuccess("user deleted");
            }
            catch (Exception exception)
            {
                return ActionConfirmation.CreateFailure("error > " + exception.Message);
            }
        }

        public ActionConfirmation Delete(string userId)
        {
            var user = _userRepository.GetById(userId);
            if (user != null)
            {
                try
                {
                    _userRepository.Delete(user);
                    _userRepository.DbContext.CommitChanges();

                    return ActionConfirmation.CreateSuccess("user deleted");
                }
                catch (Exception exception)
                {
                    return ActionConfirmation.CreateFailure("error > " + exception.Message);
                }
            }
            else
            {
               return ActionConfirmation.CreateFailure("user does not exist");
            }

        }

        public IdentityUser GetUserFromProvider(string loginProvider, string providerKey)
        {
            return _userRepository.GetUserFromLogin(loginProvider, providerKey);
        }

        public IQueryable<IdentityUser> GetUsers()
        {
            return _userRepository.GetAll().AsQueryable();
        }

        public IdentityUser GetUser(Expression<Func<IdentityUser, bool>> filter)
        {
            return _userRepository.GetUser(filter);

        }
    }
}