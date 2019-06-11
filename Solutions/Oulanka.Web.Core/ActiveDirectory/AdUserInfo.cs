using System;

namespace Oulanka.Web.Core.ActiveDirectory
{
    [Serializable]
    public class AdUserInfo
    {
        public string LoginName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitials { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string TelephoneNumber { get; set; }
        public string Extention { get; set; }
        public string Fax { get; set; }
        public string EmailAddress { get; set; }
        public string MemberCompany { get; set; }
        public string CompanyRelationship { get; set; }
        public string Status { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public string Country { get; set; }
        public byte[] Thumbnail { get; set; }
        public string DisplayName { get; set; }


    }
}