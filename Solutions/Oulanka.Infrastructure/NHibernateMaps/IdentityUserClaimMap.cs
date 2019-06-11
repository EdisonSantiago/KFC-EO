using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Oulanka.Domain.Models.Identity;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class IdentityUserClaimMap : ClassMapping<IdentityUserClaim>
    {
        public IdentityUserClaimMap()
        {
            Table("AspNetUserClaims");
            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.ClaimType);
            Property(x => x.ClaimValue);

            ManyToOne(x => x.User, m => m.Column("UserId"));
        }
    }
}