using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Oulanka.Domain.Models.Locales;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Estandares
{
    public class Estandar : EntityWithTypedId<Guid>
    {
        public virtual string Codigo { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual string NotasEspeciales { get; set; }
        public virtual short TipoEstandar { get; set; }

        // Recursividad
        public virtual Estandar EstandarPadre { get; set; }
        [ScriptIgnore]
        public virtual IEnumerable<Estandar> EstandaresHijos { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

        //Relaciones
        public virtual Estado Estado { get; set; }
        public virtual string NombreEstado => Estado != null ? Estado.Nombre : string.Empty;

        public virtual GrupoEstandar GrupoEstandar { get; set; }
        public virtual Nivel Nivel { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual Clasificacion Clasificacion { get; set; }

        [ScriptIgnore]
        public virtual IList<Sistema> Sistemas { get; set; }

        [ScriptIgnore]
        public virtual IList<TipoLocal> TipoLocales { get; set; }
        
        [ScriptIgnore]
        public virtual IList<Comentario> Comentarios { get; set; }
    }
}