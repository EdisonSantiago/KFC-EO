using System;
using Newtonsoft.Json;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models
{
    public class Estado : EntityWithTypedId<Guid>
    {
        public virtual string Nombre { get; set; }
        public virtual string Grupo { get; set; }
        public virtual string Descripcion { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }
    }
}