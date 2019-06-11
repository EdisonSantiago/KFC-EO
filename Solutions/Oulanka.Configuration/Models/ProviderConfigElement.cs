using System.Configuration;
using System.Xml;

namespace Oulanka.Configuration.Models
{
    public class ProviderConfigElement : ConfigurationElement
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RouteConfigElement" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="connectionStringName">Name of the connection string.</param>
        /// <param name="databaseOwner">The database owner.</param>
        public ProviderConfigElement(string name, string type, string connectionStringName, string databaseOwner)
        {
            Name = name;
            Type = type;
            ConnectionStringName = connectionStringName;
            DatabaseOwner = databaseOwner;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RouteConfigElement" /> class.
        /// </summary>
        /// <param name="elementName">Name of the element.</param>
        public ProviderConfigElement(string elementName)
        {
            Name = elementName;
        }

        public ProviderConfigElement()
        {
        }

        #region Properties

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string) this["name"]; }
            set { this["name"] = value; }
        }


        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return (string) this["type"]; }
            set { this["type"] = value; }
        }


        /// <summary>
        ///     Gets or sets the name of the connection string.
        /// </summary>
        /// <value>
        ///     The name of the connection string.
        /// </value>
        [ConfigurationProperty("connectionStringName", IsRequired = true)]
        public string ConnectionStringName
        {
            get { return (string) this["connectionStringName"]; }
            set { this["connectionStringName"] = value; }
        }

        [ConfigurationProperty("databaseOwner", IsRequired = false)]
        public string DatabaseOwner
        {
            get { return (string) this["databaseOwner"]; }
            set { this["databaseOwner"] = value; }
        }

        #endregion

        #region Methods


        /// <summary>
        ///     Reads XML from the configuration file.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> that reads from the configuration file.</param>
        /// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
        protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
        {
            base.DeserializeElement(reader, serializeCollectionKey);
        }

        /// <summary>
        ///     Writes the contents of this configuration element to the configuration file when implemented in a derived class.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> that writes to the configuration file.</param>
        /// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
        /// <returns>
        ///     true if any data was actually serialized; otherwise, false.
        /// </returns>
        protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
        {
            return base.SerializeElement(writer, serializeCollectionKey);
        }

        #endregion
    }
}