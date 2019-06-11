using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Domain.Contracts.Services;

namespace Oulanka.Web.Core.Helpers
{
    public static class GroupsHelper
    {
        private static readonly IUserAccountService UserAccountService =
            ServiceLocator.Current.GetInstance<IUserAccountService>();


        public static bool UserIsMemberOfGroups(string username, string[] groups)
        {
            if (groups == null || groups.Length == 0) return true;

            var user = UserAccountService.GetUser(username);

            return groups.Any(@group => user.EstaEnGrupo(@group));
        }
    }
}