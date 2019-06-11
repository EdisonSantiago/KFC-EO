using System;
using System.Collections.Specialized;
using Oulanka.Configuration.Models;

namespace Oulanka.Providers
{
    public class Provider
    {
        public Provider(ProviderConfigElement element)
        {
            if (element == null) throw new ArgumentNullException("element");
            Attributes = new NameValueCollection();

            Name = element.Name;
            Type = element.Type;          

            Attributes.Add("DatabaseOwner", element.DatabaseOwner);
            Attributes.Add("ConnectionStringName", element.ConnectionStringName);
        }

        public NameValueCollection Attributes { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
    }
}