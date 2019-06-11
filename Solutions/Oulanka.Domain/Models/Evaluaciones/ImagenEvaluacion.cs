﻿using System;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Evaluaciones
{
    public class ImagenEvaluacion : EntityWithTypedId<Guid>
    {
        public virtual string Nombre { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual string Imagen { get; set; }
        public virtual short TipoImagen { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

        //Relaciones
        public virtual Estado Estado { get; set; }
        public virtual Evaluacion Evaluacion { get; set; }
    }
} 