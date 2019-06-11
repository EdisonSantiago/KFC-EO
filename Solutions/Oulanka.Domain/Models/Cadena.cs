using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Oulanka.Domain.Models.Jerarquias;
using Oulanka.Domain.Models.Locales;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models
{
    [Serializable]
    public class Cadena : EntityWithTypedId<Guid>
    {
        public virtual string Nombre { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual DateTime FechaFundacion { get; set; }
        public virtual string Manual { get; set; }
        public virtual string Logo { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

        //Relaciones
        public virtual Estado Estado { get; set; }
        public virtual string NombreEstado => Estado != null ? Estado.Nombre : string.Empty;

        [ScriptIgnore]
        public virtual IEnumerable<Local> Locales { get; set; }
        [ScriptIgnore]
        public virtual IEnumerable<GerenteNacional> GerentesNacionales { get; set; }
    }
}