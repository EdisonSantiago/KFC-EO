using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Configuration;

namespace Oulanka.Web.Core.ActiveDirectory
{
    public sealed class AdUserInfoService
    {
        private static readonly IConfigurationSettings ConfigurationSettings = ServiceLocator.Current.GetInstance<IConfigurationSettings>();

        private AdUserInfoService()
        {
        }


        public static AdUserInfo GetAdUserInfo(string username)
        {

            using (var dirEntry = CreateDirectoryEntry())
            {
                using (var dirSearch = new DirectorySearcher(dirEntry))
                {
                    dirSearch.PropertiesToLoad.Add("sAMAccountName");

                    var dirResults = dirSearch.FindAll();
                    var dirResult = FindUserInCollection(username, dirResults);

                    if(dirResult == null) throw new Exception("The user '" + username + "' does not exist!");

                    var userInfo = PopulateAdUserInfoFromSearchResult(dirResult.GetDirectoryEntry());

                    return userInfo;

                }
            }

        }

        public static DirectoryEntry GetUserDirectoryEntry(string username)
        {
            using (var dirEntry = CreateDirectoryEntry())
            {
                using (var dirSearch = new DirectorySearcher(dirEntry))
                {
                    dirSearch.PropertiesToLoad.Add("sAMAccountName");

                    var dirResults = dirSearch.FindAll();
                    var dirResult = FindUserInCollection(username, dirResults);

                    if (dirResult == null) throw new Exception("The user '" + username + "' does not exist!");

                    return dirResult.GetDirectoryEntry();

                }
            }
        }

        public static IList<AdUserInfo> GetAdUserList()
        {
            var adUsers = new List<AdUserInfo>();

            var directoryEntry = CreateDirectoryEntry();
            var dirSearch = new DirectorySearcher(directoryEntry)
            {
                Filter = "(&(objectClass=user)(objectCategory=person))"
            };

            //dirSearch.PropertiesToLoad.Add("samaccountname");
            //dirSearch.PropertiesToLoad.Add("mail");
            //dirSearch.PropertiesToLoad.Add("usergroup");
            //dirSearch.PropertiesToLoad.Add("displayname");//first name
            
            var resultCol = dirSearch.FindAll();

            foreach (SearchResult result in resultCol)
            {
                var adUser = PopulateAdUserInfoFromSearchResult(result.GetDirectoryEntry());
                adUsers.Add(adUser);
            }

            return adUsers;
        }

        private static AdUserInfo PopulateAdUserInfoFromSearchResult(DirectoryEntry searchResult)
        {
            if (searchResult == null) throw new ArgumentNullException(nameof(searchResult));


            var userInfo = new AdUserInfo
            {
                LoginName = GetProperty(searchResult, "samaccountname"),
                FirstName = GetProperty(searchResult, "givenName"),
                MiddleInitials = GetProperty(searchResult, "initials"),
                LastName = GetProperty(searchResult, "sn"),
                Address = GetProperty(searchResult, "homePostalAddress"),
                Title = GetProperty(searchResult, "title"),
                Company = GetProperty(searchResult, "company"),
                State = GetProperty(searchResult, "st"),
                City = GetProperty(searchResult, "l"),
                Country = GetProperty(searchResult, "co"),
                PostalCode = GetProperty(searchResult, "postalCode"),
                TelephoneNumber = GetProperty(searchResult, "telephoneNumber"),
                Extention = GetProperty(searchResult, "otherTelephone"),
                Fax = GetProperty(searchResult, "facsimileTelephoneNumber"),
                EmailAddress = GetProperty(searchResult, "mail"),
                MemberCompany = GetProperty(searchResult, "extensionAttribute3"),
                CompanyRelationship = GetProperty(searchResult, "extensionAttribute4"),
                Status = GetProperty(searchResult, "extensionAttribute5"),
                CreatedOn = GetProperty(searchResult, "whenCreated"),
                UpdatedOn = GetProperty(searchResult, "whenChanged"),
                Thumbnail = GetThumbnail(searchResult, "thumbnailPhoto"),
                DisplayName = GetProperty(searchResult, "displayname")
            };

            return userInfo;

        }

        private static byte[] GetThumbnail(DirectoryEntry searchResult, string propertyName)
        {
            if (searchResult.Properties.Contains(propertyName))
            {
                var data = searchResult.Properties[propertyName].Value as byte[];

                return data;
            }

            return null;
        }

        private static SearchResult FindUserInCollection(string username, IEnumerable results)
        {
            return results.Cast<SearchResult>()
                .Where(result => 
                        result.Properties["sAMAccountName"].Count > 0)
                        .FirstOrDefault(result => 
                                result.Properties["sAMAccountName"][0].ToString() == username);
        }

        private static string GetProperty(DirectoryEntry searchResult, string propertyName)
        {
            return searchResult.Properties.Contains(propertyName)
                ? searchResult.Properties[propertyName][0].ToString()
                : string.Empty;
        }

        private static DirectoryEntry CreateDirectoryEntry()
        {
            var config = ConfigurationSettings.GetConfig();

            var ldapConnection = new DirectoryEntry(config.LdapDomain, config.LdapDomainUser, config.LdapDomainPassword,
                AuthenticationTypes.None)
            {
                Path = config.LdapConnectionPath
            };

            return ldapConnection;
        }


        public static void ResetPassword(string username, string oldPassword,string password)
        {
            var userDirectoryEntry = GetUserDirectoryEntry(username);
            userDirectoryEntry.Invoke("ChangePassword", new object[] { oldPassword,password });
            userDirectoryEntry.Properties["LockOutTime"].Value = 0; //unlock account
            userDirectoryEntry.AuthenticationType = AuthenticationTypes.None;
            userDirectoryEntry.CommitChanges();

            userDirectoryEntry.Close();
        }
    }
}