﻿using System;

namespace Oulanka.Web.Core.FormModels
{
    public class CategoriaFormModel
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public Guid EstadoId { get; set; }
    }
}