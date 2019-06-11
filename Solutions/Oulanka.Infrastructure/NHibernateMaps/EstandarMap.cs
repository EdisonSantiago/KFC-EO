using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Locales;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class EstandarMap : ClassMap<Estandar>
    {
        public EstandarMap()
        {
            Table("Estandar");

            Id(x => x.Id).GeneratedBy.GuidComb();

            // Propiedades
            Map(x => x.Codigo).Length(50).Not.Nullable().Default("''").Unique();
            Map(x => x.Nombre).Length(255).Not.Nullable().Default("''");
            Map(x => x.Descripcion).Length(10000).Not.Nullable().Default("''");
            Map(x => x.NotasEspeciales).Length(10000).Not.Nullable().Default("''");
            Map(x => x.TipoEstandar).Not.Nullable().Default("0");

            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            // Recursividad
            References(x => x.EstandarPadre);
            HasMany<Estandar>(x => x.EstandaresHijos).KeyColumn("IdEstandarPadre");

            // Relaciones
            References<Estado>(x => x.Estado).Not.Nullable();
            References<Nivel>(x => x.Nivel).Not.Nullable();
            References<Categoria>(x => x.Categoria).Not.Nullable();
            References<GrupoEstandar>(x => x.GrupoEstandar).Nullable();
            References<Clasificacion>(x => x.Clasificacion).Nullable();


            HasManyToMany<Sistema>(x => x.Sistemas)
                .Cascade.All()
                .AsBag()
                .Table("EstandarSistema");

            HasManyToMany<TipoLocal>(x => x.TipoLocales)
                .Cascade.All()
                .AsBag()
                .Table("TipoLocalEstandar");

            HasMany<Comentario>(x => x.Comentarios)
                .Cascade.All()
                .Inverse()
                .AsBag();

        }
    }
}