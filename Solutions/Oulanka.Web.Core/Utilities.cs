using System;
using System.Collections.Generic;
using Oulanka.Web.Core.Internals;

namespace Oulanka.Web.Core
{
    public static class Utilities
    {

        /// <summary>
        /// Gets the domain from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static string GetDomainFromUrl(string url)
        {
            return GetDomainFromUrl(new Uri(url));
        }

        /// <summary>
        /// Gets the domain from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="useStrict">if set to <c>true</c> [use strict].</param>
        /// <returns></returns>
        public static string GetDomainFromUrl(string url, bool useStrict)
        {
            return GetDomainFromUrl(new Uri(url),useStrict);
        }


        /// <summary>
        /// Gets the domain from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static string GetDomainFromUrl(Uri url)
        {
            return GetDomainFromUrl(url, false);
        }

        /// <summary>
        /// Gets the domain from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="useStrict">if set to <c>true</c> [use strict].</param>
        /// <returns></returns>
        public static string GetDomainFromUrl(Uri url, bool useStrict)
        {
            if (url == null) return null;

            var dotBits = url.Host.Split('.');

            if (dotBits.Length == 1) return url.Host; //eg http://localhost/ => localhost
            if (dotBits.Length == 2) return url.Host;

            var tlds = new List<string>();
            tlds.AddRange(TldPatterns.EXACT);
            tlds.AddRange(TldPatterns.UNDER);
            tlds.AddRange(TldPatterns.EXCLUDED);

            var bestMatch = "";
            foreach (var tld in tlds)
            {
                if (url.Host.EndsWith(tld, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (tld.Length > bestMatch.Length)
                        bestMatch = tld;
                }
            }

            if (string.IsNullOrEmpty(bestMatch))
                return url.Host;

            //add the domain name on to tld
            var bestBits = bestMatch.Split('.');
            var inputBits = url.Host.Split('.');
            var getLastBits = bestBits.Length + 1;

            bestMatch = "";
            for (var c = inputBits.Length - getLastBits; c < inputBits.Length; c++)
            {
                if (bestMatch.Length > 0) 
                    bestMatch += ".";

                bestMatch += inputBits[c];
            }

            return bestMatch;

        }

        public static string ReplaceSpecialChars(string value)
        {
            if(string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));

            value = value.Replace("Ñ", "N");
            value = value.Replace("ñ", "n");
            value = value.Replace("á", "a");
            value = value.Replace("Á", "A");
            value = value.Replace("é", "e");
            value = value.Replace("É", "E");
            value = value.Replace("í", "i");
            value = value.Replace("Í", "I");
            value = value.Replace("ó", "o");
            value = value.Replace("Ó", "O");
            value = value.Replace("ú", "u");
            value = value.Replace("Ú", "U");
            value = value.Replace("/", " ");
            value = value.Replace(",", " ");
            value = value.Replace(".", " ");
            value = value.Replace(":", " ");
            value = value.Replace(";", " ");
            value = value.Replace("-", " ");
            value = value.Replace("_", " ");

            return value;
            
        }
    }
}