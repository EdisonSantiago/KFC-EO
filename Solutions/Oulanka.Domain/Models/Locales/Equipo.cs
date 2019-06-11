using System;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Locales
{
    [Serializable]
    public class Equipo : EntityWithTypedId<Guid>
    {
        public virtual string Modelo { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual short Utilidad { get; set; }
        public virtual short Control { get; set; }
        public virtual int Cantidad { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

        //Relaciones
        public virtual Estado Estado { get; set; }
        public virtual string NombreEstado => Estado != null ? Estado.Nombre : string.Empty;


        public virtual TipoEquipo TipoEquipo { get; set; }
        public virtual string NombreTipoEquipo => TipoEquipo != null ? TipoEquipo.Nombre : string.Empty;

    }
}