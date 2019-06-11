using System.Collections.Generic;
using System.Web.Mvc;
using Oulanka.Domain.Models;

namespace Oulanka.Web.Core.ViewModels
{
    public class ProjectMemberViewModel
    {
        public Project Project { get; set; }
        public IList<SelectListItem> UsersToAssign { get; set; }
        public IList<SelectListItem> GroupsToAssign { get; set; }
    }
}