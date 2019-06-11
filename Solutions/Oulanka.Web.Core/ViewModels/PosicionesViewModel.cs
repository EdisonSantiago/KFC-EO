using System.Collections.Generic;
using Oulanka.Domain.Models;

namespace Oulanka.Web.Core.ViewModels
{
    public class PosicionesViewModel
    {
        public IList<Estado> Estados { get; set; }
        public IList<Cadena> Cadenas { get; set; }
    }
}