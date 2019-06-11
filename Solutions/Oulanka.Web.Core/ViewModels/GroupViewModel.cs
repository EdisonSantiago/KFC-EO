using System.Collections.Generic;
using System.Web.Mvc;
using Oulanka.Domain.Models;

namespace Oulanka.Web.Core.ViewModels
{
    public class GroupViewModel
    {
        public Grupo Grupo { get; set; }

        public IList<SelectListItem> UsersToAssign { get; set; } 
    }
}