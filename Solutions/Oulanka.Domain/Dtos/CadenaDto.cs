using System;

namespace Oulanka.Domain.Dtos
{
    public class CadenaDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaFundacion { get; set; }
        public string Manual { get; set; }
        public string Logo { get; set; }

        // auditoría micro a nivel de registro
        public string CreadoPor { get; set; }
        public string ActualizadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public DateTime ActualizadoEn { get; set; }
        
        public Guid EstadoId { get; set; }
        public string NombreEstado { get; set; }
    }
}