using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Oulanka.Domain.Models.Estandares;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Locales
{
    public class TipoLocal : EntityWithTypedId<Guid>
    {
        public virtual string Detalle { get; set; }

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
        public virtual IEnumerable<Estandar> Estandares { get; set; }
    }
}