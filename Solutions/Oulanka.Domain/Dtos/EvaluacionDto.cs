using System;

namespace Oulanka.Domain.Dtos
{
    public class EvaluacionDto
    {
        public Guid Id { get; set; }
        public DateTime FechaEvaluacion { get; set; }
        public DateTime HoraEvaluacion { get; set; }
        public Guid LocalId { get; set; }
        public string LocalCodigo { get; set; }
        public string LocalNombre { get; set; }
        public Guid CadenaId { get; set; }
        public string CadenaNombre { get; set; }
        public string NombreRGM { get; set; }
        public string NombreMIC { get; set; }
        public short ParteDelDia { get; set; }
        public string NombreParteDelDia { get; set; }
        public short TipoVisita { get; set; }
        public string CreadoPor { get; set; }
        public string ActualizadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public DateTime ActualizadoEn { get; set; }

        public Guid EstadoId { get; set; }
        public string Estado { get; set; }

        public Guid TipoEvaluacionId { get; set; }
        public string TipoEvaluacionNombre { get; set; }

        public Guid PosicionId { get; set; }
        public string PosicionNombre { get; set; }
        public string TipoVisitaNombre { get; set; }
        public Guid TipoLocalId { get; set; }
        public string TipoLocalNombre { get; set; }
    }
}