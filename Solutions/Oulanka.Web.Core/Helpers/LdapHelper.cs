using System;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Web;

namespace Oulanka.Web.Core.Helpers
{
    public static class LdapHelper
    {
        public static string GetLdapContainer()
        {
            Uri ldapUri;
            ParseLdapConnectionString(out ldapUri);

            return HttpUtility.UrlDecode(ldapUri.PathAndQuery.TrimStart('/'));
        }

        public static string GetLdapHost()
        {
            Uri ldapUri;
            ParseLdapConnectionString(out ldapUri);
            return ldapUri.Host;
        }

        public static bool ParseLdapConnectionString(out Uri ldapUri)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ADConnectionString"].ConnectionString;

            return Uri.TryCreate(connectionString, UriKind.Absolute, out ldapUri);

        }

        public static bool UserIsMemberOfGroups(string username, string[] groups)
        {
            if (groups == null || groups.Length == 0)
            {
                return true;
            }

            using (var context = BuildPrincipalContext())
            {
                var userPrincipal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, username);

                foreach (var @group in groups)
                {
                    if (userPrincipal.IsMemberOf(context, IdentityType.Name, group))
                    {
                        return true;
                    }
                }

            }

            return false;
        }

        public static PrincipalContext BuildPrincipalContext()
        {
            var container = GetLdapContainer();
            return new PrincipalContext(ContextType.Domain,null,container);
        }

    }
}