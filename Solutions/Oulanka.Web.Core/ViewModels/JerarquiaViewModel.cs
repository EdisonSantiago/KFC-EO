using System.Collections.Generic;
using Oulanka.Domain.Models;
using Oulanka.Domain.Models.Jerarquias;

namespace Oulanka.Web.Core.ViewModels
{
    public class JerarquiaViewModel
    {
        public GerenteGeneral GerenteGeneral { get; set; }
        public IList<Estado> Estados { get; set; }
        public IList<Cadena> Cadenas { get; set; }
    }
}