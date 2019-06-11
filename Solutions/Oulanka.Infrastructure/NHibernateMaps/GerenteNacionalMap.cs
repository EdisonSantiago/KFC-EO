using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Jerarquias;
using Oulanka.Domain.Models.Locales;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class GerenteNacionalMap : ClassMap<GerenteNacional>
    {
        public GerenteNacionalMap()
        {
            Table("GerenteNacional");

            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Nombre).Not.Nullable().Length(50).Default("''");
            Map(x => x.Descripcion).Not.Nullable().Length(255).Default("''");


            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            // Relaciones
            References<Estado>(x => x.Estado).Not.Nullable();
            References<Cadena>(x => x.Cadena).Not.Nullable();
            References<GerenteGeneral>(x => x.GerenteGeneral).Not.Nullable();
            HasMany<GerenteRegional>(x => x.GerentesRegionales).Inverse().Cascade.All().AsBag();
        }



    }
}