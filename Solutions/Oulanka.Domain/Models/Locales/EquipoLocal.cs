using System;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Locales
{
    public class EquipoLocal : EntityWithTypedId<Guid>
    {
        public virtual Local Local { get; set; }
        public virtual Equipo Equipo { get; set; }
        public virtual short Utilidad { get; set; }
        public virtual short Control { get; set; }
        public virtual int Cantidad { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }
    }
}