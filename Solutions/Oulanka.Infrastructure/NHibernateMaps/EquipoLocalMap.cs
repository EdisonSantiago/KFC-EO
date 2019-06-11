using System;
using FluentNHibernate.Mapping;
using Oulanka.Domain.Models.Locales;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class EquipoLocalMap : ClassMap<EquipoLocal>
    {
        public EquipoLocalMap()
        {
            Table("EquipoLocal");
            
            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue(Guid.Empty);
            Map(x => x.Utilidad).Not.Nullable().Default("0");
            Map(x => x.Control).Not.Nullable().Default("0");
            Map(x => x.Cantidad).Not.Nullable().Default("0");
            
            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            References<Equipo>(x => x.Equipo).Cascade.All().Not.Nullable();
            References<Local>(x => x.Local).Cascade.All().Not.Nullable();
        }
    }
}