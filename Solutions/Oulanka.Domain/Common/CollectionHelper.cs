using System;
using System.Collections;
using System.Net.Mail;
using System.Text;

namespace Oulanka.Domain.Common
{
    public static class CollectionHelper
    {
        public static string ToDelimitedString(this ICollection collection, string delimiter)
        {
            var delimitedString = new StringBuilder();
            if (collection is Hashtable)
            {
                foreach (object o in ((Hashtable)collection).Keys)
                {
                    delimitedString.Append(o + delimiter);
                }
            }

            if (collection is ArrayList)
            {
                foreach (var o in (ArrayList)collection)
                {
                    delimitedString.Append(o + delimiter);
                }
            }

            if (collection is string[])
            {
                foreach (string s in (string[])collection)
                {
                    delimitedString.Append(s + delimiter);
                }
            }

            if (collection is MailAddressCollection)
            {
                foreach (MailAddress address in collection)
                {
                    delimitedString.Append(address.Address + delimiter);
                }
            }


            return delimitedString.ToString().TrimEnd(Convert.ToChar(delimiter));
        }
    }
}