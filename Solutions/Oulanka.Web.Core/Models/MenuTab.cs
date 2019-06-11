using System;

namespace Oulanka.Web.Core.Models
{
    [Serializable]
    public class MenuTab
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public string CssClass { get; set; }
        public object RouteValues { get; set; }
        public string Text { get; set; }


        private MenuTab(string text, string action, string controller, string cssClass, object routeValues)
        {
            Text = text;
            Action = action;
            Controller = controller;
            CssClass = cssClass;
            RouteValues = routeValues;
        }


        public static MenuTab Create(string text, string action, string controller)
        {
            return Create(text, action, controller, string.Empty);
        }

        public static MenuTab Create(string text, string action, string controller, string cssClass)
        {
            return Create(text, action, controller, cssClass, null);
        }

        public static MenuTab Create(string text, string action, string controller, string cssClass, object routeValues)
        {
            return new MenuTab(text,action, controller,cssClass,routeValues);
        }
    }
}