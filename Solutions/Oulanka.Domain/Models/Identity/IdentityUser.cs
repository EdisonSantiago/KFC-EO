using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Identity
{
    public class IdentityUser : EntityWithTypedId<string>, IUser

    {
        public virtual int AccessFailedCount { get; set; }

        public virtual string Email { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual DateTime? LockoutEndDateUtc { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual bool PhoneNumberConfirmed { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        public virtual string UserName { get; set; }

        public virtual string SecurityStamp { get; set; }

        public virtual ICollection<IdentityRole> Roles { get; protected set; }

        public virtual ICollection<IdentityUserClaim> Claims { get; protected set; }

        public virtual ICollection<IdentityUserLogin> Logins { get; protected set; }

        public IdentityUser()
        {
            this.Roles = new List<IdentityRole>();
            this.Claims = new List<IdentityUserClaim>();
            this.Logins = new List<IdentityUserLogin>();
        }

        public IdentityUser(string userName)
            : this()
        {
            this.UserName = userName;
        }

    }
}