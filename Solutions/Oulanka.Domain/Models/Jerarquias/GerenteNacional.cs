using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using NHibernate.SqlTypes;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Jerarquias
{
    public class GerenteNacional : EntityWithTypedId<Guid>
    {
        public virtual string Nombre { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual Estado Estado { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

        public virtual Cadena Cadena { get; set; }
        public virtual string CadenaNombre
        {
            get { return Cadena != null ? Cadena.Nombre : string.Empty; }
        }
        public virtual GerenteGeneral GerenteGeneral { get; set; }

        [ScriptIgnore]
        public virtual IList<GerenteRegional> GerentesRegionales { get; set; }
    }
}