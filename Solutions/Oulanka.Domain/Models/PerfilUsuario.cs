using System;
using System.Web.Script.Serialization;
using FluentNHibernate.Data;

namespace Oulanka.Domain.Models
{
    [Serializable]
    public class PerfilUsuario : Entity
    {

        public virtual string Nombre { get; set; }
        public virtual string Apellido { get; set; }
        public virtual string Direccion { get; set; }
        public virtual string Telefono { get; set; }
        public virtual string Imagen { get; set; }

        [ScriptIgnore]
        public virtual Usuario Usuario { get; set; }

    }
}