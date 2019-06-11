using System;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Security
{
    public class AplicacionCliente : EntityWithTypedId<Guid>
    {
        public virtual string AppId { get; set; }
        public virtual string Secret { get; set; }
        public virtual string Nombre { get; set; }
        public virtual short TipoAplicacion { get; set; }
        public virtual bool EstaActiva { get; set; }
        public virtual int RefreshTokenLifeTime { get; set; }
        public virtual string AllowedOrigin { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

    }
}