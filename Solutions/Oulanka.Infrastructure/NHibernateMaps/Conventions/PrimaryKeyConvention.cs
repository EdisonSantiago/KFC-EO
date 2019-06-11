using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Oulanka.Infrastructure.NHibernateMaps.Conventions
{
    public class PrimaryKeyConvention : IIdConvention
    {

        public void Apply(IIdentityInstance instance)
        {
            instance.Column("Id" + instance.EntityType.Name );
        }

    }
}