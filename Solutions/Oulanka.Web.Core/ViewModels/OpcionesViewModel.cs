using System.Collections.Generic;
using Oulanka.Domain.Models;

namespace Oulanka.Web.Core.ViewModels
{
    public class OpcionesViewModel
    {
        public IList<Estado> Estados { get; set; }
        public IDictionary<int,string> TipoOpciones { get; set; }

        public OpcionesViewModel()
        {
            Estados = new List<Estado>();
            TipoOpciones = new Dictionary<int, string>();
        }
    }
}