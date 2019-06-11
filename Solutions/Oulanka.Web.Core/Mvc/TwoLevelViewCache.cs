using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Oulanka.Web.Core.Mvc
{
    public class TwoLevelViewCache : IViewLocationCache
    {

        private static readonly object SKey = new object();
        private readonly IViewLocationCache _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwoLevelViewCache"/> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
        public TwoLevelViewCache(IViewLocationCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Gets the request cache.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns></returns>
        private static IDictionary<string, string> GetRequestCache(HttpContextBase httpContext)
        {
            var cacheDictionary = httpContext.Items[SKey] as IDictionary<string, string>;

            if (cacheDictionary == null)
            {
                cacheDictionary = new Dictionary<string, string>();
                httpContext.Items[SKey] = cacheDictionary;
            }

            return cacheDictionary;
        }

        /// <summary>
        /// Gets the view location by using the specified HTTP context and the cache key.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="key">The cache key.</param>
        /// <returns>
        /// The view location.
        /// </returns>
        public string GetViewLocation(HttpContextBase httpContext, string key)
        {
            var dictionary = GetRequestCache(httpContext);
            string location;

            if (!dictionary.TryGetValue(key, out location))
            {
                location = _cache.GetViewLocation(httpContext, key);
                dictionary[key] = location;
            }

            return location;
        }

        /// <summary>
        /// Inserts the specified view location into the cache by using the specified HTTP context and the cache key.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="key">The cache key.</param>
        /// <param name="virtualPath">The virtual path.</param>
        public void InsertViewLocation(HttpContextBase httpContext, string key, string virtualPath)
        {
            _cache.InsertViewLocation(httpContext, key, virtualPath);
        }

    }
}