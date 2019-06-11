using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Identity
{
    public class IdentityUserLogin : Entity
    {
        public virtual string LoginProvider { get; set; }

        public virtual string ProviderKey { get; set; }
    }
}