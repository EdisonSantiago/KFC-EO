using FluentNHibernate.Mapping;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Personal;

namespace Oulanka.Infrastructure.NHibernateMaps
{
    public class PersonaMap : ClassMap<Persona>
    {
        public PersonaMap()
        {
            Table("Persona");

            // Propiedades
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Nombre).Not.Nullable().Length(50).Default("''");
            Map(x => x.Apellido).Not.Nullable().Length(255).Default("''");
            Map(x => x.FechaNacimiento).Not.Nullable().Default("getdate()");
            Map(x => x.Email).Not.Nullable().Length(255).Default("''");
            Map(x => x.Telefono).Not.Nullable().Length(20).Default("''");
            Map(x => x.Direccion).Not.Nullable().Length(255).Default("''");
            Map(x => x.Fotografia).Not.Nullable().Length(255).Default("''");

            // Auditoria a nivel de registro
            Map(x => x.CreadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.ActualizadoPor).Length(50).Not.Nullable().Default("''");
            Map(x => x.CreadoEn).Not.Nullable().Default("getdate()");
            Map(x => x.ActualizadoEn).Not.Nullable().Default("getdate()");

            // Relaciones
            References<Estado>(x => x.Estado).Not.Nullable();


        }
    }
}