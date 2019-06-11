using System;

namespace Oulanka.Web.Core.FormModels
{
    public class JefeAreaFormModel
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Guid EstadoId { get; set; }
        public Guid RegionalId { get; set; }
    }
}