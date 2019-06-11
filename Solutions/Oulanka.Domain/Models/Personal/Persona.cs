using System;
using System.Collections.Generic;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Personal
{
    public class Persona : EntityWithTypedId<Guid>
    {
        public virtual Guid Id { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Apellido { get; set; }
        public virtual DateTime FechaNacimiento { get; set; }
        public virtual string Email { get; set; }
        public virtual string Telefono { get; set; }
        public virtual string Direccion { get; set; }
        public virtual string Fotografia { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

        //Relaciones
        public virtual Estado Estado { get; set; }
    }
}