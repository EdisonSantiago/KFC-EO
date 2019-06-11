using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Identity
{
    public class IdentityUserClaim : EntityWithTypedId<int>
    {
        public virtual string ClaimType { get; set; }

        public virtual string ClaimValue { get; set; }

        public virtual IdentityUser User { get; set; }
    }
}