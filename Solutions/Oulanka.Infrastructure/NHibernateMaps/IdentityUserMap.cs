using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Oulanka.Domain.Models.Identity;
using Cascade = NHibernate.Mapping.ByCode.Cascade;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class IdentityUserMap : ClassMapping<IdentityUser>
    {
        public IdentityUserMap()
        {
            Table("AspNetUsers");
            Id(x => x.Id, m => { m.Generator(Generators.Guid);m.UnsavedValue(Guid.Empty); });

            Property(x => x.AccessFailedCount);

            Property(x => x.Email);

            Property(x => x.EmailConfirmed);

            Property(x => x.LockoutEnabled);

            Property(x => x.LockoutEndDateUtc);

            Property(x => x.PasswordHash);

            Property(x => x.PhoneNumber);

            Property(x => x.PhoneNumberConfirmed);

            Property(x => x.TwoFactorEnabled);

            Property(x => x.UserName, map =>
            {
                map.Length(255);
                map.NotNullable(true);
                map.Unique(true);
            });

            Property(x => x.SecurityStamp);

            Bag(x => x.Claims, map =>
            {
                map.Key(k =>
                {
                    k.Column("UserId");
                    k.Update(false); // to prevent extra update afer insert
                });
                map.Cascade(Cascade.All | Cascade.DeleteOrphans);
            }, rel =>
            {
                rel.OneToMany();
            });

            Set(x => x.Logins, cam =>
            {
                cam.Table("AspNetUserLogins");
                cam.Key(km => km.Column("UserId"));
                cam.Cascade(Cascade.All | Cascade.DeleteOrphans);
            },
                     map =>
                     {
                         map.Component(comp =>
                         {
                             comp.Property(p => p.LoginProvider);
                             comp.Property(p => p.ProviderKey);
                         });
                     });

            Bag(x => x.Roles, map =>
            {
                map.Table("AspNetUserRoles");
                map.Key(k => k.Column("UserId"));
            }, rel => rel.ManyToMany(p => p.Column("RoleId")));
        }
    }
}