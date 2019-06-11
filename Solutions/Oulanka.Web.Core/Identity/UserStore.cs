using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Domain.Contracts.Services;
using Oulanka.Domain.Models.Identity;

namespace Oulanka.Web.Core.Identity
{
    public class UserStore<TUser> : IUserLoginStore<TUser>, IUserClaimStore<TUser>, IUserRoleStore<TUser>,
        IUserPasswordStore<TUser>, IUserSecurityStampStore<TUser>, IQueryableUserStore<TUser>, IUserStore<TUser>,
        IUserLockoutStore<TUser, string>, IUserEmailStore<TUser>, IUserPhoneNumberStore<TUser>,
        IUserTwoFactorStore<TUser, string>, IDisposable where TUser : IdentityUser
    {
        private bool _disposed;
        private IIdentityUserService _userService = ServiceLocator.Current.GetInstance<IIdentityUserService>();
        private readonly IIdentityRoleService _roleService = ServiceLocator.Current.GetInstance<IIdentityRoleService>();

        /// <summary>
        /// Gets or sets a value indicating whether [should dispose session].
        /// </summary>
        /// <value>
        /// <c>true</c> if [should dispose session]; otherwise, <c>false</c>.
        /// </value>
        public bool ShouldDisposeSession { get; set; }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public IQueryable<TUser> Users
        {
            get
            {
                ThrowIfDisposed();
                return _userService.GetUsers() as IQueryable<TUser>;
            }
        }

        public UserStore()
        {
            ShouldDisposeSession = true;
        }

        public virtual Task<TUser> FindByIdAsync(string userId)
        {
            ThrowIfDisposed();
            return GetUserAggregateAsync((IdentityUser u) => u.Id.Equals(userId)) as Task<TUser>;
        }

        public virtual Task<TUser> FindByNameAsync(string userName)
        {
            ThrowIfDisposed();
            return GetUserAggregateAsync((IdentityUser u) => u.UserName.ToUpper() == userName.ToUpper()) as Task<TUser>;
        }

        public virtual Task CreateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            var confirmation = _userService.SaveOrUpdate(user);

            if (confirmation.WasSuccessful)
                return Task.FromResult(0);
            else
                throw new Exception(confirmation.Message);
        }

        public virtual Task DeleteAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var confirmation = _userService.Delete(user);

            if (confirmation.WasSuccessful)
                return Task.FromResult(0);
            else
                throw new Exception(confirmation.Message);
        }

        public virtual Task UpdateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            var confirmation = _userService.SaveOrUpdate(user);
            if (confirmation.WasSuccessful)
                return Task.FromResult(0);
            else
                throw new Exception(confirmation.Message);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _userService != null && ShouldDisposeSession)
            {
                _userService.Dispose();
            }
            _disposed = true;
            _userService = null;
        }

        public virtual Task<TUser> FindAsync(UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (login == null) throw new ArgumentNullException(nameof(login));

            var user = _userService.GetUserFromProvider(login.LoginProvider, login.ProviderKey);

            return Task.FromResult(user as TUser);
        }

        public virtual Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            ThrowIfDisposed();

            if (user == null) throw new ArgumentNullException(nameof(user));
            if (login == null) throw new ArgumentNullException(nameof(login));

            user.Logins.Add(new IdentityUserLogin()
            {
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider
            });

            var confirmation = _userService.SaveOrUpdate(user);
            if (confirmation.WasSuccessful)
                return Task.FromResult(0);
            else
                throw new Exception(confirmation.Message);
        }

        public virtual Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (login == null) throw new ArgumentNullException(nameof(login));

            var info =
                user.Logins.SingleOrDefault(
                    x => x.LoginProvider == login.LoginProvider &&
                         x.ProviderKey == login.ProviderKey);

            if (info != null)
            {
                user.Logins.Remove(info);
                _userService.SaveOrUpdate(user);
            }

            return Task.FromResult(0);
        }

        public virtual Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            var result = (from identityUserLogin in (IEnumerable<IdentityUserLogin>)user.Logins
                          select new UserLoginInfo(identityUserLogin.LoginProvider, identityUserLogin.ProviderKey))
                .ToList();

            return Task.FromResult<IList<UserLoginInfo>>(result);
        }

        public virtual Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            var result = user.Claims
                .Select(identityUserClaim => new Claim(identityUserClaim.ClaimType, identityUserClaim.ClaimValue))
                .ToList();

            return Task.FromResult<IList<Claim>>(result);
        }

        public virtual Task AddClaimAsync(TUser user, Claim claim)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (claim == null) throw new ArgumentNullException(nameof(claim));

            user.Claims.Add(new IdentityUserClaim()
            {
                User = user,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            });

            return Task.FromResult(0);
        }

        public virtual Task RemoveClaimAsync(TUser user, Claim claim)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (claim == null) throw new ArgumentNullException(nameof(claim));

            foreach (var identityUserClaim in user.Claims.Where(uc =>
            {
                if (uc.ClaimValue == claim.Value)
                    return uc.ClaimType == claim.Type;

                return false;
            }).ToList())
            {
                user.Claims.Remove(identityUserClaim);
            }

            return Task.FromResult(0);
        }

        public virtual Task AddToRoleAsync(TUser user, string role)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(role)) throw new ArgumentNullException(nameof(role));

            var identityRole = _roleService.GetRoleByName(role);
            if (identityRole == null)
            {
                throw new InvalidOperationException($"Role Not Found {role}");
            }

            user.Roles.Add(identityRole);

            return Task.FromResult(0);
        }

        public virtual Task RemoveFromRoleAsync(TUser user, string role)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(role)) throw new ArgumentNullException(nameof(role));

            var identityUserRole = _roleService.GetRoleByName(role);
            if (identityUserRole != null)
                user.Roles.Remove(identityUserRole);

            return Task.FromResult(0);
        }

        public virtual Task<IList<string>> GetRolesAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult((IList<string>)user.Roles.Select(u => u.Name).ToList());
        }

        public virtual Task<bool> IsInRoleAsync(TUser user, string role)
        {
            ThrowIfDisposed();

            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(role)) throw new ArgumentNullException(nameof(role));

            return Task.FromResult(user.Roles.Any(r => r.Name.ToUpper() == role.ToUpper()));
        }

        public virtual Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetPasswordHashAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PasswordHash);
        }

        public virtual Task SetSecurityStampAsync(TUser user, string stamp)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetSecurityStampAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.SecurityStamp);

        }

        public virtual Task<bool> HasPasswordAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PasswordHash != null);
        }

        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.LockoutEnabled);
        }

        public virtual Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            DateTimeOffset dateTimeOffset;
            if (user.LockoutEndDateUtc.HasValue)
            {
                var lockoutEndDateUtc = user.LockoutEndDateUtc;
                dateTimeOffset = new DateTimeOffset(DateTime.SpecifyKind(lockoutEndDateUtc.Value, DateTimeKind.Utc));
            }
            else
            {
                dateTimeOffset = new DateTimeOffset();
            }

            return Task.FromResult(dateTimeOffset);
        }

        public virtual Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            user.AccessFailedCount = user.AccessFailedCount + 1;

            return Task.FromResult(user.AccessFailedCount);
        }


        public virtual Task ResetAccessFailedCountAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public virtual Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        public virtual Task SetLockoutEnabledAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            var nullable = lockoutEnd == DateTimeOffset.MinValue
                ? null
                : new DateTime?(lockoutEnd.UtcDateTime);

            user.LockoutEndDateUtc = nullable;

            return Task.FromResult(0);
        }

        public virtual Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {

            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            var nullable = lockoutEnd == DateTimeOffset.MinValue 
                ? null
                : new DateTime?(lockoutEnd.UtcDateTime);

            user.LockoutEndDateUtc = nullable;
            return Task.FromResult(0);
        }


        public virtual Task<TUser> FindByEmailAsync(string email)
        {
            ThrowIfDisposed();
            return GetUserAggregateAsync(u => u.Email.ToUpper() == email.ToUpper()) as Task<TUser>;
        }

        public virtual Task<string> GetEmailAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            ThrowIfDisposed();

            return Task.FromResult(user.Email);
        }

        public virtual Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.EmailConfirmed);
        }

        public virtual Task SetEmailAsync(TUser user, string email)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            ThrowIfDisposed();

            user.Email = email;

            return Task.FromResult(0);
        }

        public virtual Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            ThrowIfDisposed();

            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetPhoneNumberAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            ThrowIfDisposed();

            return Task.FromResult(user.PhoneNumber);
        }

        public virtual Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            ThrowIfDisposed();

            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public virtual Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            ThrowIfDisposed();

            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public virtual Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            ThrowIfDisposed();

            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public virtual Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            ThrowIfDisposed();

            return Task.FromResult(user.TwoFactorEnabled);
        }

        public virtual Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            ThrowIfDisposed();

            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        private Task<IdentityUser> GetUserAggregateAsync(Expression<Func<IdentityUser, bool>> filter)
        {
            return Task.Run(() => _userService.GetUser(filter));
        }
    }
}