using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Oulanka.Infrastructure.NHibernateMaps.Conventions
{
    public class TableNameConvention : IClassConvention
    {

        public void Apply(IClassInstance instance)
        {
            //instance.Table(instance.EntityType.Name.InflectTo().Pluralized);
            instance.Table(instance.EntityType.Name);
        }

    }
}