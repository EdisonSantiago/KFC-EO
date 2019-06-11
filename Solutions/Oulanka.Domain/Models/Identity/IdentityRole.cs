using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Identity
{
    public class IdentityRole : EntityWithTypedId<string>, IRole
    {
        public virtual string Name { get; set; }

        public virtual ICollection<IdentityUser> Users { get; protected set; }

        public IdentityRole()
        {
            this.Users = (ICollection<IdentityUser>)new List<IdentityUser>();
        }

        public IdentityRole(string roleName) : this()
        {
            this.Name = roleName;
        }
    }
}