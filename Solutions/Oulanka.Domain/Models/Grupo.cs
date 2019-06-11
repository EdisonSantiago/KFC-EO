using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models
{
    public class Grupo : Entity 
    {
        public virtual string Nombre { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }

        [ScriptIgnore]
        public virtual IList<Usuario> Usuarios { get; set; } 
    }
}