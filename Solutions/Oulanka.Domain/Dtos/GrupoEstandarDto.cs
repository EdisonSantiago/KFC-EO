using System;

namespace Oulanka.Domain.Dtos
{
    public class GrupoEstandarDto
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string CreadoPor { get; set; }
        public string ActualizadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public DateTime ActualizadoEn { get; set; }
        public Guid EstadoId { get; set; }
        public string EstadoNombre { get; set; }
    }
}