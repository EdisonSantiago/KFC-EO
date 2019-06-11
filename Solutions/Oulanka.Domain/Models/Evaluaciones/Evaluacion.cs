using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Oulanka.Domain.Enums;
using Oulanka.Domain.Models.Estandares;
using Oulanka.Domain.Models.Locales;
using Oulanka.Domain.Models.Personal;
using Oulanka.Domain.Models.Respuestas;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Evaluaciones
{
    public class Evaluacion : EntityWithTypedId<Guid>
    {
        public virtual DateTime FechaEvaluacion { get; set; }
        public virtual DateTime HoraEvaluacion { get; set; }
        public virtual string NombreRGM { get; set; }
        public virtual string NombreMIC { get; set; }
        
        public virtual short ParteDelDia { get; set; }
        public virtual string NombreParteDelDia => Enum.GetName(typeof(ParteDelDia), ParteDelDia);

        public virtual short TipoVisita { get; set; }
        public virtual string NombreTipoVisita => Enum.GetName(typeof(TipoVisita), TipoVisita);

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

        //Relaciones
        public virtual Estado Estado { get; set; }
        public virtual string NombreEstado => Estado != null ? Estado.Nombre : string.Empty;

        public virtual TipoEvaluacion TipoEvaluacion { get; set; }
        public virtual string NombreTipoEvaluacion => TipoEvaluacion != null ? TipoEvaluacion.Nombre : string.Empty;


        public virtual Posicion PosicionMIC { get; set; }
        public virtual string NombrePosicionMIC => PosicionMIC != null ? PosicionMIC.Nombre : string.Empty;


        public virtual Local Local { get; set; }
        public virtual string NombreLocal => Local != null ? Local.Nombre : string.Empty;


        [ScriptIgnore]
        public virtual IEnumerable<Respuesta> Respuestas { get; set; }

        [ScriptIgnore]
        public virtual IEnumerable<ImagenEvaluacion> Imagenes { get; set; }
    }
}