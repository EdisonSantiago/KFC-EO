using System;
using System.Configuration;
using Oulanka.Configuration;
using Oulanka.Configuration.Models;

namespace Oulanka.Services
{
    /// <summary>
    /// Configuration Settings Service
    /// </summary>
    public class ConfigurationSettings : IConfigurationSettings
    {

        public ConfigurationSettings(){}

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ApplicationException">Required \project\ config section missing</exception>
        public BaseConfigurationSection GetConfig()
        {
            var result = ConfigurationManager.GetSection("project");
            if (result == null)
            {
                throw new ApplicationException("Required \"project\" config section missing");
            }

            return (BaseConfigurationSection)result;
        }

        /// <summary>
        /// Gets the route table.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ApplicationException">Can't find section <routeTable> in the configuration file</exception>
        public RouteTableSection GetRouteTable()
        {
            var routesTableSection = (RouteTableSection)ConfigurationManager.GetSection("routeTable");

            if (routesTableSection == null)
            {
                throw new ApplicationException("Can't find section <routeTable> in the configuration file");
            }

            return routesTableSection;
        }

        /// <summary>
        /// Gets the provider table.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ApplicationException">Can't find section <providers> in the configuration file</exception>
        public ProviderTableSection GetProviderTable()
        {
            var providerTableSection = (ProviderTableSection) ConfigurationManager.GetSection("providerTable");

            if (providerTableSection == null)
            {
                throw new ApplicationException("Can't find section <providers> in the configuration file");
            }
            return providerTableSection;
        }
    }
}