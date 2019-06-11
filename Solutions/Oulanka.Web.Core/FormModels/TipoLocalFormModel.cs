using System;

namespace Oulanka.Web.Core.FormModels
{
    public class TipoLocalFormModel
    {
        public Guid Id { get; set; }
        public string Detalle { get; set; }
        public Guid EstadoId { get; set; }
    }
}