using FluentNHibernate.Mapping;
using Oulanka.Domain.Models.Security;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class AplicacionClienteMap : ClassMap<AplicacionCliente>
    {
        public AplicacionClienteMap()
        {
            Table("AplicacionCliente");

            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.AppId).Not.Nullable().Length(50).Default("''");
            Map(x => x.Secret).Not.Nullable().Length(500).Default("''");
            Map(x => x.Nombre).Not.Nullable().Length(100).Default("''");
            Map(x => x.TipoAplicacion).Not.Nullable().Default("0");
            Map(x => x.EstaActiva).Not.Nullable().Default("0");
            Map(x => x.RefreshTokenLifeTime).Not.Nullable().Default("0");
            Map(x => x.AllowedOrigin).Not.Nullable().Length(500).Default("''");

            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

        }
    }
}