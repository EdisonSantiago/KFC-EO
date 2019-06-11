using Oulanka.Domain.Models;

namespace Oulanka.Web.Core.ViewModels
{
    public class UserStatusViewModel
    {
        public bool IsAuthenticated { get; set; }
        public string LoginName { get; set; }
        public string DisplayName { get; set; }
        public string UserDisplay { get; set; }
        public byte[] Thumbnail { get; set; }

        public string Email { get; set; }
        public Usuario Usuario { get; set; }

        public bool IsOperatorOrAdmin => !Usuario.EstaEnGrupo("users");
    }
}