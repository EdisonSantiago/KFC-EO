using System;
using System.ComponentModel.DataAnnotations;

namespace Oulanka.Web.Core.FormModels
{
    public class TipoEvaluacionFormModel
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Observaciones { get; set; }
        public Guid EstadoId { get; set; }
    }
}