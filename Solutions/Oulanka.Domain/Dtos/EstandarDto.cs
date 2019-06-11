using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Estandares;

namespace Oulanka.Domain.Dtos
{
    public class EstandarDto
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string NotasEspeciales { get; set; }
        public short TipoEstandar { get; set; }
        public Guid? EstandarPadreId { get; set; }
        public string EstandarPadreNombre { get; set; }
        public string CreadoPor { get; set; }
        public string ActualizadoPor { get; set; }
        public DateTime CreadoEn { get; set; }
        public DateTime ActualizadoEn { get; set; }
        public Guid EstadoId { get; set; }
        public string EstadoNombre { get; set; }
        public Guid GrupoEstandarId { get; set; }
        public string GrupoEstandarNombre { get; set; }
        public Guid NivelId { get; set; }
        public string NivelNombre { get; set; }
        public Guid CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }
        public Guid ClasificacionId { get; set; }        
        public string ClasificacionNombre { get; set; }        
        public IEnumerable<EstandarDto> EstandaresHijos { get; set; }
        public bool HasChildren => EstandaresHijos.Any(x=> x.TipoEstandar == (short)Enums.TipoEstandar.Estandar);
    }
}