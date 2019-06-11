using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class ValorAtributoMap : ClassMap<ValorAtributo>
    {
        public ValorAtributoMap()
        {
            Table("ValorAtributo");

            // Propiedades
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Entidad).Not.Nullable().Length(255).Default("''");
            Map(x => x.Valor).Not.Nullable().Length(10000).Default("''");
            Map(x => x.Extra).Not.Nullable().Length(10000).Default("''");

            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            References<Atributo>(x => x.Atributo).Not.Nullable();

        }
    }
}