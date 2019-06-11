using System.Configuration;
using System.Xml;

namespace Oulanka.Configuration.Models
{
    public class JobItemConfigurationElement : ConfigurationElement
    {
        public JobItemConfigurationElement()
        {
        }

        public JobItemConfigurationElement(string name, string type, int minutes, bool enabled)
        {
            Name = name;
            Type = type;
            Minutes = minutes;
            Enabled = enabled;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JobItemConfigurationElement" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public JobItemConfigurationElement(string name)
        {
            Name = name;
        }

        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether [enable shut down].
        /// </summary>
        /// <value><c>true</c> if [enable shut down]; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("enableShutDown", DefaultValue = false, IsRequired = true)]
        public bool EnableShutDown
        {
            get { return (bool) this["enableShutDown"]; }
            set { this["enableShutDown"] = value; }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="JobItemConfigurationElement" /> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("enabled", DefaultValue = true, IsRequired = true)]
        public bool Enabled
        {
            get { return (bool) this["enabled"]; }
            set { this["enabled"] = value; }
        }

        [ConfigurationProperty("minutes", DefaultValue = 1, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 10000, ExcludeRange = false)]
        public int Minutes
        {
            get { return (int) this["minutes"]; }
            set { this["minutes"] = value; }
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [ConfigurationProperty("name", DefaultValue = "Job", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string) this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [single thread].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [single thread]; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("singleThread", DefaultValue = false, IsRequired = false)]
        public bool SingleThread
        {
            get { return (bool) this["singleThread"]; }
            set { this["singleThread"] = value; }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [ConfigurationProperty("type", DefaultValue = "Type", IsRequired = true)]
        public string Type
        {
            get { return (string) this["type"]; }
            set { this["type"] = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Reads XML from the configuration file.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> that reads from the configuration file.</param>
        /// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
        /// <exception cref="T:System.Configuration.ConfigurationErrorsException">The element to read is locked.- or -An attribute
        /// of the current node is not recognized.- or -The lock status of the current node cannot be determined.</exception>
        protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
        {
            base.DeserializeElement(reader, serializeCollectionKey);

            // Custom Processing
        }

        /// <summary>
        ///     Indicates whether this configuration element has been modified since it was last saved or loaded, when implemented
        ///     in a derived class.
        /// </summary>
        /// <returns>
        ///     true if the element has been modified; otherwise, false.
        /// </returns>
        protected override bool IsModified()
        {
            bool ret = base.IsModified();

            // custom processing

            return ret;
        }

        /// <summary>
        ///     Writes the contents of this configuration element to the configuration file when implemented in a derived class.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> that writes to the configuration file.</param>
        /// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
        /// <returns>
        ///     true if any data was actually serialized; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.Configuration.ConfigurationErrorsException">
        ///     The current attribute is locked at a higher
        ///     configuration level.
        /// </exception>
        protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
        {
            bool ret = base.SerializeElement(writer, serializeCollectionKey);

            // custom processing

            return ret;
        }

        #endregion
    }
}