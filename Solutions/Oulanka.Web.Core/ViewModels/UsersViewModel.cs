using System.Collections.Generic;
using Oulanka.Domain.Models;
using Oulanka.Web.Core.ActiveDirectory;

namespace Oulanka.Web.Core.ViewModels
{
    public class UsersViewModel
    {
        public IList<Usuario> Users { get; set; }
        public IList<AdUserInfo> AdUsers { get; set; }
        public bool IsLdapAuthEnabled { get; set; }
    }
}