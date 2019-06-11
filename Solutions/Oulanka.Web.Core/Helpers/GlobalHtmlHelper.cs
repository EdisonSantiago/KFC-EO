using System.Text;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Oulanka.Configuration;

namespace Oulanka.Web.Core.Helpers
{
    public static class GlobalHtmlHelper
    {
        private static readonly IConfigurationSettings Configuration = ServiceLocator.Current.GetInstance<IConfigurationSettings>();

        public static MvcHtmlString ApplicationName(this HtmlHelper html)
        {
            var value = Configuration.GetConfig().ApplicationName;
            return new MvcHtmlString(value);
        }

        public static MvcHtmlString TrueOrFalse(this HtmlHelper html, bool value)
        {
            var str = "fa-";
            str = str + (value ? "check" : "ban");
            var builder1 = new StringBuilder();
            builder1.Append("<i class=\"fa " + str + "\"></i>");
            return new MvcHtmlString(builder1.ToString());
        }

    }
}
