using System.Configuration;
using System.Xml;

namespace Oulanka.Configuration.Models
{
    public class RouteConfigElement : ConfigurationElement
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Gets or sets the constraints.
        /// </summary>
        /// <value>
        /// The constraints.
        /// </value>
        [ConfigurationProperty("constraints", IsRequired = false)]
        public RouteChildElement Constraints
        {
            get { return (RouteChildElement)this["constraints"]; }
            set { this["constraints"] = value; }
        }

        /// <summary>
        /// Gets or sets the data tokens.
        /// </summary>
        /// <value>
        /// The data tokens.
        /// </value>
        [ConfigurationProperty("dataTokens", IsRequired = false)]
        public RouteChildElement DataTokens
        {
            get { return (RouteChildElement)this["dataTokens"]; }
            set { this["dataTokens"] = value; }
        }

        /// <summary>
        /// Gets or sets the defaults.
        /// </summary>
        /// <value>
        /// The defaults.
        /// </value>
        [ConfigurationProperty("defaults", IsRequired = false)]
        public RouteChildElement Defaults
        {
            get { return (RouteChildElement)this["defaults"]; }
            set { this["defaults"] = value; }
        }

        /// <summary>
        /// Gets or sets the type of the route handler.
        /// </summary>
        /// <value>
        /// The type of the route handler.
        /// </value>
        [ConfigurationProperty("routeHandlerType", IsRequired = false)]
        public string RouteHandlerType
        {
            get { return (string)this["routeHandlerType"]; }
            set { this["routeHandlerType"] = value; }
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get { return (string)this["url"]; }
            set { this["url"] = value; }
        } 

        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="RouteConfigElement"/> class.
        /// </summary>
        /// <param name="newName">The new name.</param>
        /// <param name="newUrl">The new URL.</param>
        /// <param name="routeHandlerType">Type of the route handler.</param>
        public RouteConfigElement(string newName, string newUrl, string routeHandlerType)
        {
            this.Name = newName;
            this.Url = newUrl;
            this.RouteHandlerType = routeHandlerType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteConfigElement"/> class.
        /// </summary>
        /// <param name="elementName">Name of the element.</param>
        public RouteConfigElement(string elementName)
        {
            this.Name = elementName;
        }

        public RouteConfigElement() { }

        #region Methods

        /// <summary>
        /// Reads XML from the configuration file.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> that reads from the configuration file.</param>
        /// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
        protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
        {
            base.DeserializeElement(reader, serializeCollectionKey);
        }

        /// <summary>
        /// Writes the contents of this configuration element to the configuration file when implemented in a derived class.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> that writes to the configuration file.</param>
        /// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
        /// <returns>
        /// true if any data was actually serialized; otherwise, false.
        /// </returns>
        protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
        {
            return base.SerializeElement(writer, serializeCollectionKey);
        }

        #endregion

    }
}
