using System;

namespace Oulanka.Api.Models.FormModels
{
    public class EvaluacionFormModel
    {
        public Guid TipoEvaluacion { get; set; }
        public string NombreRGM { get; set; }
        public string NombreMIC { get; set; }
        public Guid Posicion { get; set; }
        public short TipoVisita { get; set; }
        public short ParteDia { get; set; }
        public Guid IdLocal { get; set; }
    }
}