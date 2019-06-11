using System.Configuration;

namespace Oulanka.Configuration.Models
{
    public class JobsConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [ConfigurationProperty("items")]
        public JobItemsCollection Items
        {
            get
            {
                var items = (JobItemsCollection)this["items"];
                return items;
            }
        }

        /// <summary>
        /// Gets the minutes.
        /// </summary>
        /// <value>
        /// The minutes.
        /// </value>
        [ConfigurationProperty("minutes", DefaultValue = 1, IsRequired = true)]
        public int Minutes
        {
            get
            {
                return (int)this["minutes"];
            }
        }

        /// <summary>
        /// Gets a value indicating whether [single thread].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [single thread]; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("singleThread", DefaultValue = true, IsRequired = true)]
        public bool SingleThread
        {
            get
            {
                return (bool)this["singleThread"];
            }
        }
    }
}