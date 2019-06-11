using System;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Evaluaciones;

namespace Oulanka.Domain.Dtos
{
    public class ImagenEvaluacionDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public short TipoImagen { get; set; }
        public string CreadoPor { get; set; }
        public string ActualizadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public DateTime ActualizadoEn { get; set; }
        public Guid EstadoId { get; set; }
        public string EstadoNombre { get; set; }
        public Guid EvaluacionId { get; set; }
    }
}