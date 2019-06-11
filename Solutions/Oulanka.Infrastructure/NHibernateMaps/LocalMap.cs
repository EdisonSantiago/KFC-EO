using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Evaluaciones;
using Oulanka.Domain.Models.Jerarquias;
using Oulanka.Domain.Models.Locales;
using Oulanka.Domain.Models.Ubicacion;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class LocalMap : ClassMap<Local>
    {
        public LocalMap()
        {
            Table("Local");

            // Propiedades
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Codigo).Not.Nullable().Length(50).Default("''");
            Map(x => x.Nombre).Not.Nullable().Length(50).Default("''");
            Map(x => x.Descripcion).Not.Nullable().Length(255).Default("''");
            Map(x => x.Imagen).Not.Nullable().Length(255).Default("''");
            Map(x => x.Direccion).Not.Nullable().Length(255).Default("''");
            Map(x => x.Telefono).Not.Nullable().Length(255).Default("''");
            Map(x => x.Email).Not.Nullable().Length(255).Default("''");
            Map(x => x.Ruc).Not.Nullable().Length(255).Default("''");
            Map(x => x.Logo).Not.Nullable().Length(255).Default("''");
            Map(x => x.OpClave).Not.Nullable().Length(255).Default("''");
            Map(x => x.Propietario).Not.Nullable().Length(255).Default("''");
            Map(x => x.AC).Not.Nullable().Length(255).Default("''");
            Map(x => x.Concepto).Not.Nullable().Default("0");

            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            // Relaciones
            References<Estado>(x => x.Estado).Not.Nullable();
            References<TipoLocal>(x => x.TipoLocal).Not.Nullable();
            References<Cadena>(x => x.Cadena).Not.Nullable();
            References<Ciudad>(x => x.Ciudad).Not.Nullable();
            References<JefeArea>(x => x.JefeArea).Not.Nullable();

            HasMany<Equipo>(x => x.Equipos).Inverse().Cascade.All().AsBag();
            HasMany<Evaluacion>(x => x.Evaluaciones).Inverse().Cascade.All().AsBag();
            HasMany<ImagenLocal>(x => x.Imagenes).Inverse().Cascade.All().AsBag();
        }
    }
}