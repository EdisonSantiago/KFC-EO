using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Ubicacion
{
    [Serializable]
    public class Region : EntityWithTypedId<Guid>
    {
        public virtual string Nombre { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

        //Relaciones
        public virtual Estado Estado { get; set; }
        public virtual string NombreEstado => Estado != null ? Estado.Nombre : string.Empty;

        [ScriptIgnore]
        public virtual IEnumerable<Provincia> Provincias { get; set; }

    }
}