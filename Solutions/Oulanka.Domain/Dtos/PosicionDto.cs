using System;

namespace Oulanka.Domain.Dtos
{
    public class PosicionDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string CreadoPor { get; set; }
        public string ActualizadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public DateTime ActualizadoEn { get; set; }
        public string NombreEstado { get; set; }
        public Guid IdEstado { get; set; }
        public string NombreCadena{ get; set; }
        public Guid IdCadena { get; set; }
    }
}