using FluentNHibernate.Mapping;
using Oulanka.Domain.Models.Locales;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class ImagenLocalMap : ClassMap<ImagenLocal>
    {
        public ImagenLocalMap()
        {
            Table("ImagenLocal");

            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Descripcion).Not.Nullable().Length(255).Default("''");
            Map(x => x.Imagen).Not.Nullable().Length(255).Default("''");
            Map(x => x.Tipo).Not.Nullable().Default("0");
            Map(x => x.Orden).Not.Nullable().Default("0");

            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            // Relaciones
            References<Local>(x => x.Local).Not.Nullable();


        }
    }
}