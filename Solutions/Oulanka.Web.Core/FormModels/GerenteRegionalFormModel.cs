using System;

namespace Oulanka.Web.Core.FormModels
{
    public class GerenteRegionalFormModel
    {
        public Guid Id { get; set; }
        public Guid NacionalId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Guid EstadoId { get; set; }
    }
}