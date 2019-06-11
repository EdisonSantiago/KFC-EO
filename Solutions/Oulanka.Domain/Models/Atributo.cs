using System;
using System.Collections.Generic;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models
{
    public class Atributo : EntityWithTypedId<Guid>
    {
        public virtual Guid Id { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual string Etiqueta { get; set; }
        public virtual short TipoDeDato { get; set; }
        public virtual string Entidad { get; set; }
        public virtual string Datos { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

        public virtual IEnumerable<ValorAtributo> Valores { get; set; }
    }
}