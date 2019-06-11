using System.Collections.Generic;
using System.Configuration;

namespace Oulanka.Configuration.Models
{
    /// <summary>
    /// Route Child Element
    /// </summary>
    public class RouteChildElement : ConfigurationElement
    {
        /// <summary>
        /// The _attributes
        /// </summary>
        private readonly Dictionary<string, string> _attributes = new Dictionary<string, string>();

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public Dictionary<string, string> Attributes
        {
            get { return this._attributes; }
        }

        /// <summary>
        /// Gets a value indicating whether an unknown attribute is encountered during deserialization.
        /// </summary>
        /// <param name="name">The name of the unrecognized attribute.</param>
        /// <param name="value">The value of the unrecognized attribute.</param>
        /// <returns>
        /// true when an unknown attribute is encountered while deserializing; otherwise, false.
        /// </returns>
        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            this._attributes.Add(name, value);
            return true;
        }
    }
}