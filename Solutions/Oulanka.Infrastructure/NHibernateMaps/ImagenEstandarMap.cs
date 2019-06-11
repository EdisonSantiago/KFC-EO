using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Respuestas;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class ImagenEstandarMap : ClassMap<ImagenEstandar>
    {
        public ImagenEstandarMap()
        {
            Table("ImagenEstandar");

            Id(x => x.Id).GeneratedBy.GuidComb();

            // Propiedades
            Map(x => x.Nombre).Length(50).Not.Nullable().Default("''");
            Map(x => x.Descripcion).Length(255).Not.Nullable().Default("''");
            Map(x => x.Imagen).Length(255).Not.Nullable().Default("''");
            Map(x => x.TipoImagen).Not.Nullable().Default("0");

            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            // Relaciones
            References<Estado>(x => x.Estado).Not.Nullable();
            References<Respuesta>(x => x.Respuesta).Not.Nullable();


        }
    }
}