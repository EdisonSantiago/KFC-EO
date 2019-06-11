using System;
using System.Configuration;

namespace Oulanka.Configuration.Models
{
    /// <summary>
    /// Route Collection Configuration Element
    /// </summary>
    public class RouteCollection : ConfigurationElementCollection
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the <see cref="T:System.Configuration.ConfigurationElement" /> to associate with the add operation in the <see cref="T:System.Configuration.ConfigurationElementCollection" /> when overridden in a derived class.
        /// </summary>
        /// <returns>The name of the element.</returns>
        public new string AddElementName
        {
            get { return base.AddElementName; }
            set { base.AddElementName = value; }
        }

        /// <summary>
        /// Gets or sets the name for the <see cref="T:System.Configuration.ConfigurationElement" /> to associate with the clear operation in the <see cref="T:System.Configuration.ConfigurationElementCollection" /> when overridden in a derived class.
        /// </summary>
        /// <returns>The name of the element.</returns>
        public new string ClearElementName
        {
            get { return base.ClearElementName; }
            set { base.AddElementName = value; }
        }

        /// <summary>
        /// Gets the type of the <see cref="T:System.Configuration.ConfigurationElementCollection" />.
        /// </summary>
        /// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> of this collection.</returns>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        /// <summary>
        /// Gets the number of elements in the collection.
        /// </summary>
        /// <returns>The number of elements in the collection.</returns>
        public new int Count
        {
            get { return base.Count; }
        }

        /// <summary>
        /// Gets or sets the name of the <see cref="T:System.Configuration.ConfigurationElement" /> to associate with the remove operation in the <see cref="T:System.Configuration.ConfigurationElementCollection" /> when overridden in a derived class.
        /// </summary>
        /// <returns>The name of the element.</returns>
        public new string RemoveElementName
        {
            get { return base.RemoveElementName; }
        }

        #endregion

        #region Indexers

        /// <summary>
        /// Gets or sets the <see cref="RouteConfigElement"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="RouteConfigElement"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public RouteConfigElement this[int index]
        {
            get { return (RouteConfigElement)this.BaseGet(index); }
            set
            {
                if (this.BaseGet(index) != null)
                    this.BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }

        /// <summary>
        /// Gets the <see cref="RouteConfigElement"/> with the specified name.
        /// </summary>
        /// <value>
        /// The <see cref="RouteConfigElement"/>.
        /// </value>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public new RouteConfigElement this[string name]
        {
            get { return (RouteConfigElement)BaseGet(name); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        public void Add(RouteConfigElement url) { this.BaseAdd(url); }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear() { this.BaseClear(); }

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public int IndexOf(RouteConfigElement url) { return this.BaseIndexOf(url); }

        /// <summary>
        /// Removes the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        public void Remove(RouteConfigElement url)
        {
            if (this.BaseIndexOf(url) >= 0)
                this.BaseRemove(url.Name);
        }

        /// <summary>
        /// Removes the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public void Remove(string name) { this.BaseRemove(name); }

        /// <summary>
        /// Removes at.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveAt(int index) { this.BaseRemoveAt(index); }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a configuration element to the <see cref="T:System.Configuration.ConfigurationElementCollection" />.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to add.</param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            this.BaseAdd(element, false);
        }

        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new RouteConfigElement();
        }

        /// <summary>
        /// Creates a new <see cref="T:System.Configuration.ConfigurationElement" /> when overridden in a derived class.
        /// </summary>
        /// <param name="elementName">The name of the <see cref="T:System.Configuration.ConfigurationElement" /> to create.</param>
        /// <returns>
        /// A new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new RouteConfigElement(elementName);
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((RouteConfigElement)element).Name;
        }

        #endregion

    }
}