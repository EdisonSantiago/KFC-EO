using System;

namespace Oulanka.Api.Models.FormModels
{
    public class ImagenLocalFormModel
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string ImagenData { get; set; }
        public short Tipo { get; set; }
        public int Orden { get; set; }
        public string CreadoPor { get; set; }
        public string ActualizadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public DateTime ActualizadoEn { get; set; }

        public Guid LocalId { get; set; }
        public string LocalNombre { get; set; }
        public string LocalCodigo { get; set; }
    }
}