using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Oulanka.Domain.Models.Locales;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Estandares
{
    public class GrupoEstandar : EntityWithTypedId<Guid>
    {
        [DomainSignature]
        public virtual string Codigo { get; set; }

        public virtual string Nombre { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual string Imagen { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

        //Relaciones
        public virtual Estado Estado { get; set; }

        [ScriptIgnore]
        public virtual IList<Estandar> Estandares { get; set; }
        public virtual string NombreEstado => Estado != null ? Estado.Nombre : string.Empty;


        public GrupoEstandar()
        {
            Descripcion = string.Empty;
            Imagen = string.Empty;
        }
    }
}