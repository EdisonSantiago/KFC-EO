using System.Collections.Generic;
using Oulanka.Domain.Models;

namespace Oulanka.Web.Core.ViewModels
{
    public class RoleViewModel
    {
        public IList<Grupo> Groups { get; set; }
    }
}