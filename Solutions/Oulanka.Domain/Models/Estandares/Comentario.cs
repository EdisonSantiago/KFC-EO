using System;
using System.Collections.Generic;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Estandares
{
    public class Comentario : EntityWithTypedId<Guid>
    {
        public virtual string Nombre { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual string Valor { get; set; }
        public virtual short TipoComentario { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

        //Relaciones
        public virtual Estado Estado { get; set; }
        public virtual Estandar Estandar { get; set; }
        public virtual IEnumerable<Opcion> Opciones { get; set; }
    }
}