using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Configuration;

namespace Oulanka.Web.Core.Helpers
{
    public static class ResourcesHelper
    {
        private static readonly IConfigurationSettings Configuration = ServiceLocator.Current.GetInstance<IConfigurationSettings>();

        public static MvcHtmlString Resource(this HtmlHelper html, string name)
        {
            return Resource(html,name,false);
        }

        public static MvcHtmlString Resource(this HtmlHelper html, string name, bool defaultOnly)
        {
            return Resource(html, name,Configuration.GetConfig().ResourcesFile, false);
        }

        public static MvcHtmlString Resource(this HtmlHelper html, string name, string fileName)
        {
            return Resource(html, name, fileName, false);

        }

        public static MvcHtmlString Resource(this HtmlHelper html, string name, string fileName,bool defaultOnly)
        {
            return new MvcHtmlString(ResourceManager.GetString(name, fileName, defaultOnly));
        }

        
    }
}