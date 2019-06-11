using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Oulanka.Infrastructure.NHibernateMaps.Conventions
{
    public class HasManyConvention : IHasManyConvention
    {

        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Key.Column("Id" + instance.EntityType.Name);
            instance.Cascade.AllDeleteOrphan();
            instance.Inverse();
        }

    }
}