using System;

namespace Oulanka.Domain.Dtos
{
    public class LocalDto
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Ruc { get; set; }
        public string Logo { get; set; }
        public string OpClave { get; set; }
        public string Propietario { get; set; }
        public string AC { get; set; }
        public short Concepto { get; set; }
        public string CreadoPor { get; set; }
        public string ActualizadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public DateTime ActualizadoEn { get; set; }

        public string EstadoNombre { get; set; }
        public Guid EstadoId { get; set; }

        public string TipoLocalDetalle{ get; set; }
        public Guid TipoLocalId { get; set; }

        public string CadenaNombre { get; set; }
        public Guid CadenaId { get; set; }

        public string CiudadNombre { get; set; }
        public Guid CiudadId { get; set; }

        public string JefeAreaNombre { get; set; }
        public Guid JefeAreaId { get; set; }
    }
}