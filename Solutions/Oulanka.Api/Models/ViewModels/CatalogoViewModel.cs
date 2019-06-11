using System.Collections.Generic;
using Oulanka.Domain.Dtos;
using Oulanka.Domain.Models.Personal;

namespace Oulanka.Api.Models.ViewModels
{
    public class CatalogoViewModel
    {
        public IList<PosicionDto> Posiciones { get; set; }
        public IList<NameValue> PartesDia { get; set; }
        public IList<NameValue> TiposVisita { get; set; }
    }
}