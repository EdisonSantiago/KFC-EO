using System;

namespace Oulanka.Web.Core.FormModels
{
    public class SistemaFormModel
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Guid EstadoId { get; set; }
    }
}