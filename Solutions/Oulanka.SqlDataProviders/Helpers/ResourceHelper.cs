using System.IO;
using System.Reflection;

namespace Oulanka.Data.Helpers
{
    public static class ResourceHelper
    {
        public static string GetEmbeddedResource(string resourceName)
        {
            return GetEmbeddedResource(resourceName, Assembly.GetCallingAssembly());
        }

        public static string GetEmbeddedResource(string resourceName, Assembly assembly)
        {
            resourceName = FormatResourceName(resourceName);
            using (var resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (resourceStream == null) return null;

                using (var reader = new StreamReader(resourceStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }


        public static byte[] GetEmbeddedResourceAsBytes(string resourceName)
        {
            return GetEmbeddedResourceAsBytes(resourceName, Assembly.GetCallingAssembly());
        }

        public static byte[] GetEmbeddedResourceAsBytes(string resourceName, Assembly assembly)
        {
            resourceName = FormatResourceName(resourceName);
            using (var resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (resourceStream == null) return null;

                var content = new byte[resourceStream.Length];
                resourceStream.Read(content, 0, content.Length);

                return content;
            }
        }

        private static string FormatResourceName(string resourceName)
        {
            return "BslAdmin.Data.SqlQueries." + resourceName.Replace(" ", "_").Replace("\\", ".").Replace("/", ".");
        }
    }
}