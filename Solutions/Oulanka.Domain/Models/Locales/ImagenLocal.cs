﻿using System;
using SharpArch.Domain.DomainModel;

namespace Oulanka.Domain.Models.Locales
{
    public class ImagenLocal : EntityWithTypedId<Guid>
    {
        public virtual string Descripcion { get; set; }
        public virtual string Imagen { get; set; }
        public virtual short Tipo { get; set; }
        public virtual int Orden { get; set; }

        // auditoría micro a nivel de registro
        public virtual string CreadoPor { get; set; }
        public virtual string ActualizadoPor { get; set; }
        public virtual DateTime CreadoEn { get; set; }
        public virtual DateTime ActualizadoEn { get; set; }

        public virtual Local Local { get; set; }
    }
}