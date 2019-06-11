using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Oulanka.Domain.Models.Evaluaciones;
using Oulanka.Domain.Models.Jerarquias;
using Oulanka.Domain.Models.Ubicacion;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Locales
{
    public class Local : EntityWithTypedId<Guid>
    {
        public virtual string Codigo { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual string Imagen { get; set; }
        public virtual string Direccion { get; set; }
        public virtual string Telefono { get; set; }
        public virtual string Email { get; set; }
        public virtual string Ruc { get; set; }
        public virtual string Logo { get; set; }
        public virtual string OpClave { get; set; }
        public virtual string Propietario { get; set; }
        public virtual string AC { get; set; }
        public virtual short Concepto { get; set; }


        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

        //Relaciones
        public virtual Estado Estado { get; set; }
        public virtual TipoLocal TipoLocal { get; set; }
        public virtual Cadena Cadena { get; set; }
        public virtual Ciudad Ciudad { get; set; }
        public virtual JefeArea JefeArea { get; set; }

        [ScriptIgnore]
        public virtual IEnumerable<Equipo> Equipos { get; set; }

        [ScriptIgnore]
        public virtual IEnumerable<Evaluacion> Evaluaciones { get; set; }

        [ScriptIgnore]
        public virtual IEnumerable<ImagenLocal> Imagenes { get; set; }
    }
}