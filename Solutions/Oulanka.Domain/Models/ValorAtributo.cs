using System;
using System.Collections.Generic;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models
{
    public class ValorAtributo : EntityWithTypedId<Guid>
    {
        public virtual Guid Id { get; set; }
        public virtual string Entidad { get; set; }
        public virtual string Valor { get; set; }
        public virtual string Extra { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

        public virtual Atributo Atributo { get; set; }

    }
}