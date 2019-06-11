using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace Oulanka.Web.Core
{
    public class AppCache
    {
        private static readonly Cache _cache;

        public const int DayFactor = 17280;
        public const int HourFactor = 720;
        public const int MinuteFactor = 12;
        public const double SecondFactor = 0.2;
        public static int Factor = 5;

        static AppCache()
        {
            var context = HttpContext.Current;
            _cache = context != null ? context.Cache : HttpRuntime.Cache;
        }

        public static void Clear()
        {
            var cacheEnum = _cache.GetEnumerator();
            var keyList = new List<object>();

            while (cacheEnum.MoveNext())
            {
                keyList.Add(cacheEnum.Key);
            }

            foreach (string obj in keyList)
            {
                _cache.Remove(obj);
            }
        }

        public static void Remove(string key)
        {
            _cache.Remove(key);
        }


        public static void RemoveByPattern(string pattern)
        {
            var cacheEnum = _cache.GetEnumerator();
            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
            while (cacheEnum.MoveNext())
            {
                if (regex.IsMatch(cacheEnum.Key.ToString()))
                    _cache.Remove(cacheEnum.Key.ToString());
            }

        }

        public static object Get(string key)
        {
            return _cache[key];
        }

        public static T Get<T>(string key) where T : class
        {
            return _cache[key] as T;
        }


        public static int SecondFactorCalculate(int seconds)
        {
            // Insert method below takes integer seconds, so we have to round any fractional values
            return Convert.ToInt32(Math.Round(seconds * SecondFactor));
        }


        public static void Insert(string key, object obj)
        {
            Insert(key, obj, null, 1);
        }


        public static void Insert(string key, object obj, CacheDependency dep)
        {
            Insert(key, obj, dep, HourFactor * 12);
        }

        public static void Insert(string key, object obj, int seconds)
        {
            Insert(key, obj, null, seconds);
        }

        public static void Insert(string key, object obj, int seconds, CacheItemPriority priority)
        {
            Insert(key, obj, null, seconds, priority);
        }


        public static void Insert(string key, object obj, CacheDependency dep, int seconds)
        {
            Insert(key, obj, dep, seconds, CacheItemPriority.Normal);
        }

        public static void Insert(string key, object obj, CacheDependency dep, int seconds, CacheItemPriority priority)
        {
            if (obj != null)
            {
                _cache.Insert(key, obj, dep, DateTime.Now.AddSeconds(Factor * seconds), TimeSpan.Zero, priority, null);
            }

        }



        public static void MicroInsert(string key, object obj, int secondFactor)
        {
            if (obj != null)
            {
                _cache.Insert(key, obj, null, DateTime.Now.AddSeconds(Factor * secondFactor), TimeSpan.Zero);
            }
        }


        public static void Max(string key, object obj)
        {
            Max(key, obj, null);
        }

        public static void Max(string key, object obj, CacheDependency dep)
        {
            if (obj != null)
            {
                _cache.Insert(key, obj, dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.AboveNormal, null);
            }
        }
    }
}