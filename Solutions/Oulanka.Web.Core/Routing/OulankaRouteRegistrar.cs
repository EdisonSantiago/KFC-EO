using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Configuration;
using Oulanka.Configuration.Models;

namespace Oulanka.Web.Core.Routing
{
    public class OulankaRouteRegistrar
    {
        private static readonly IConfigurationSettings Configuration =
            ServiceLocator.Current.GetInstance<IConfigurationSettings>();

        /// <summary>
        /// Gets the data tokens.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns></returns>
        private static RouteValueDictionary GetDataTokens(RouteConfigElement route)
        {
            return GetDictionaryFromAttributes(route.DataTokens.Attributes);
        }

        /// <summary>
        /// Gets the constraints.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns></returns>
        private static RouteValueDictionary GetConstraints(RouteConfigElement route)
        {
            return GetDictionaryFromAttributes(route.Constraints.Attributes);
        }

        /// <summary>
        /// Gets the defaults.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns></returns>
        private static RouteValueDictionary GetDefaults(RouteConfigElement route)
        {
            return GetDictionaryFromAttributes(route.Defaults.Attributes);
        }

        /// <summary>
        /// Gets the dictionary from attributes.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <returns></returns>
        private static RouteValueDictionary GetDictionaryFromAttributes(Dictionary<string, string> attributes)
        {
            var dataTokenDictionary = new RouteValueDictionary();
            foreach (var dataToken in attributes)
            {
                switch (dataToken.Key)
                {
                    case "namespaces":
                        dataTokenDictionary.Add(dataToken.Key, new[] { dataToken.Value });
                        break;
                    default:
                        dataTokenDictionary.Add(dataToken.Key, dataToken.Value);
                        break;
                }
            }

            return dataTokenDictionary;
        }

        /// <summary>
        /// Gets the instance of route handler.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns></returns>
        /// <exception cref="System.ApplicationException"></exception>
        private static IRouteHandler GetInstanceOfRouteHandler(RouteConfigElement route)
        {
            IRouteHandler routeHandler;

            if (string.IsNullOrEmpty(route.RouteHandlerType))
            {
                routeHandler = new MvcRouteHandler();
            }
            else
            {
                try
                {
                    var routeHandlerType = Type.GetType(route.RouteHandlerType);
                    routeHandler = Activator.CreateInstance(routeHandlerType) as IRouteHandler;
                }
                catch (Exception exception)
                {
                    throw new ApplicationException(
                        string.Format("Can not create an instace of IRouteHandler {0}", route.RouteHandlerType), exception);
                }
            }

            return routeHandler;
        }



        /// <summary>
        /// Registers from configuration route table.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterFromConfigRouteTable(System.Web.Routing.RouteCollection routes)
        {
            var routesTableSection = Configuration.GetRouteTable();
            if (routesTableSection != null && routesTableSection.Routes.Count > 0)
            {
                for (var routeIndex = 0; routeIndex < routesTableSection.Routes.Count; routeIndex++)
                {
                    var route = routesTableSection.Routes[routeIndex];

                    routes.Add(
                            route.Name,
                            new Route(
                                route.Url,
                                GetDefaults(route),
                                GetConstraints(route),
                                GetDataTokens(route),
                                GetInstanceOfRouteHandler(route)
                                ));
                }
            }
        }
    }
}