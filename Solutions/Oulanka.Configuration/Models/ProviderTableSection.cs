using System.Configuration;
using System.Xml;

namespace Oulanka.Configuration.Models
{
    /// <summary>
    ///     Route Table Section
    /// </summary>
    public class ProviderTableSection : ConfigurationSection
    {
        private ProviderConfigElement _provider;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RouteTableSection" /> class.
        /// </summary>
        public ProviderTableSection()
        {
            _provider = new ProviderConfigElement();
        }

        #region Properties

        /// <summary>
        ///     Gets the routes.
        /// </summary>
        /// <value>
        ///     The routes.
        /// </value>
        [ConfigurationProperty("providers", IsDefaultCollection = false)]
        public ProviderCollection Providers
        {
            get
            {
                var urlsCollection = (ProviderCollection) base["providers"];
                return urlsCollection;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Reads XML from the configuration file.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object, which reads from the configuration file.</param>
        protected override void DeserializeSection(XmlReader reader)
        {
            base.DeserializeSection(reader);
        }

        /// <summary>
        ///     Creates an XML string containing an unmerged view of the <see cref="T:System.Configuration.ConfigurationSection" />
        ///     object as a single section to write to a file.
        /// </summary>
        /// <param name="parentElement">
        ///     The <see cref="T:System.Configuration.ConfigurationElement" /> instance to use as the
        ///     parent when performing the un-merge.
        /// </param>
        /// <param name="name">The name of the section to create.</param>
        /// <param name="saveMode">
        ///     The <see cref="T:System.Configuration.ConfigurationSaveMode" /> instance to use when writing to
        ///     a string.
        /// </param>
        /// <returns>
        ///     An XML string containing an unmerged view of the <see cref="T:System.Configuration.ConfigurationSection" /> object.
        /// </returns>
        protected override string SerializeSection(
            ConfigurationElement parentElement, string name, ConfigurationSaveMode saveMode)
        {
            return base.SerializeSection(parentElement, name, saveMode);
        }

        #endregion
    }
}