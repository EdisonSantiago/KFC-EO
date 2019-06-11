using System.Configuration;

namespace Oulanka.Configuration.Models
{
    /// <summary>
    ///     Route Collection Configuration Element
    /// </summary>
    public class ProviderCollection : ConfigurationElementCollection
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the name of the <see cref="T:System.Configuration.ConfigurationElement" /> to associate with the add
        ///     operation in the <see cref="T:System.Configuration.ConfigurationElementCollection" /> when overridden in a derived
        ///     class.
        /// </summary>
        /// <returns>The name of the element.</returns>
        public new string AddElementName
        {
            get { return base.AddElementName; }
            set { base.AddElementName = value; }
        }

        /// <summary>
        ///     Gets or sets the name for the <see cref="T:System.Configuration.ConfigurationElement" /> to associate with the
        ///     clear operation in the <see cref="T:System.Configuration.ConfigurationElementCollection" /> when overridden in a
        ///     derived class.
        /// </summary>
        /// <returns>The name of the element.</returns>
        public new string ClearElementName
        {
            get { return base.ClearElementName; }
            set { base.AddElementName = value; }
        }

        /// <summary>
        ///     Gets the type of the <see cref="T:System.Configuration.ConfigurationElementCollection" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> of this collection.</returns>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        /// <summary>
        ///     Gets the number of elements in the collection.
        /// </summary>
        /// <returns>The number of elements in the collection.</returns>
        public new int Count
        {
            get { return base.Count; }
        }

        /// <summary>
        ///     Gets or sets the name of the <see cref="T:System.Configuration.ConfigurationElement" /> to associate with the
        ///     remove operation in the <see cref="T:System.Configuration.ConfigurationElementCollection" /> when overridden in a
        ///     derived class.
        /// </summary>
        /// <returns>The name of the element.</returns>
        public new string RemoveElementName
        {
            get { return base.RemoveElementName; }
        }

        #endregion

        #region Indexers


        /// <summary>
        /// Gets or sets the <see cref="ProviderConfigElement"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="ProviderConfigElement"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public ProviderConfigElement this[int index]
        {
            get { return (ProviderConfigElement) BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }


        /// <summary>
        ///     Gets the <see cref="ProviderConfigElement" /> with the specified name.
        /// </summary>
        /// <value>
        ///     The <see cref="ProviderConfigElement" />.
        /// </value>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public new ProviderConfigElement this[string name]
        {
            get { return (ProviderConfigElement) BaseGet(name); }
        }

        #endregion

        #region Public Methods


        /// <summary>
        /// Adds the specified provider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public void Add(ProviderConfigElement provider)
        {
            BaseAdd(provider);
        }

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        public void Clear()
        {
            BaseClear();
        }



        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        public int IndexOf(ProviderConfigElement provider)
        {
            return BaseIndexOf(provider);
        }



        /// <summary>
        /// Removes the specified provider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public void Remove(ProviderConfigElement provider)
        {
            if (BaseIndexOf(provider) >= 0)
                BaseRemove(provider.Name);
        }

        /// <summary>
        ///     Removes the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public void Remove(string name)
        {
            BaseRemove(name);
        }

        /// <summary>
        ///     Removes at.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds a configuration element to the <see cref="T:System.Configuration.ConfigurationElementCollection" />.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to add.</param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }


        /// <summary>
        ///     When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </summary>
        /// <returns>
        ///     A new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ProviderConfigElement();
        }


        /// <summary>
        ///     Creates a new <see cref="T:System.Configuration.ConfigurationElement" /> when overridden in a derived class.
        /// </summary>
        /// <param name="elementName">The name of the <see cref="T:System.Configuration.ConfigurationElement" /> to create.</param>
        /// <returns>
        ///     A new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new ProviderConfigElement(elementName);
        }

        /// <summary>
        ///     Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for.</param>
        /// <returns>
        ///     An <see cref="T:System.Object" /> that acts as the key for the specified
        ///     <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProviderConfigElement) element).Name;
        }

        #endregion
    }
}