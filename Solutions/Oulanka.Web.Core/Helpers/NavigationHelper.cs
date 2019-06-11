using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Oulanka.Web.Core.Models;

namespace Oulanka.Web.Core.Helpers
{
    public static class NavigationHelper
    {
        #region Tabbed Default Menu
        public static MvcHtmlString TabbedMenu(this HtmlHelper helper, IEnumerable<MenuTab> tabs)
        {
            return TabbedMenu(helper, tabs, string.Empty);
        }

        public static MvcHtmlString TabbedMenu(this HtmlHelper helper, IEnumerable<MenuTab> tabs, string menuId)
        {
            return TabbedMenu(helper, tabs, menuId, false);
        }

        public static MvcHtmlString TabbedMenu(
            this HtmlHelper helper, IEnumerable<MenuTab> tabs, string menuId, bool singleController)
        {
            return TabbedMenu(helper, tabs, menuId, string.Empty, singleController);
        }


        public static MvcHtmlString TabbedMenu(
            this HtmlHelper helper,
            IEnumerable<MenuTab> tabs,
            string menuId,
            string menuCssClass,
            bool singleController)
        {
            var route = helper.ViewContext.IsChildAction 
                ? helper.ViewContext.ParentActionViewContext.RouteData 
                : helper.ViewContext.RouteData;

            var controller = route.GetRequiredString("controller");
            var action = route.GetRequiredString("action");

            var ulTemplateFormat = "<ul id=\"{0}\" class=\"{1}\" >";


            var menuSb = new StringBuilder();
            menuSb.AppendFormat(ulTemplateFormat, menuId, menuCssClass);

            var index = 0;
            foreach (var tab in tabs)
            {
                var first = index <= 0 ? "first" : string.Empty;

                if (!singleController)
                {
                    if ((controller.ToLower() == tab.Controller.ToLower() && action.ToLower() == tab.Action.ToLower()) || controller.ToLower() == tab.Controller.ToLower())
                    {
                        menuSb.AppendFormat(
                            "\n\t<li class=\"{1} launcher active\"><i class=\"{2}\"></i>{0}</li>",
                            helper.ActionLink(
                                tab.Text,
                                tab.Action,
                                tab.Controller,
                                tab.RouteValues,
                                new
                                {
                                    @class = " active",
                                    @title = tab.Text
                                }), first, tab.CssClass);
                    }
                    else
                        menuSb.AppendFormat(
                            "\n\t<li class=\"{1} launcher\"><i class=\"{2}\"></i>{0}</li>",
                            helper.ActionLink(
                                tab.Text,
                                tab.Action,
                                tab.Controller,
                                tab.RouteValues,
                                new
                                {
                                    @title = tab.Text
                                }), first, tab.CssClass);
                }
                else
                {
                    if ((controller.ToLower() == tab.Controller.ToLower() && action.ToLower() == tab.Action.ToLower()))
                    {
                        menuSb.AppendFormat(
                            "\n\t<li class=\"{1} launcher active\"><i class=\"{2}\"></i>{0}</li>",
                            helper.ActionLink(
                                tab.Text,
                                tab.Action,
                                tab.Controller,
                                tab.RouteValues,
                                new
                                {
                                    @class = " active",
                                    @title = tab.Text
                                }), first, tab.CssClass);
                    }
                    else
                        menuSb.AppendFormat(
                            "\n\t<li class=\"{1} launcher \"><i class=\"{2}\"></i>{0}</li>",
                            helper.ActionLink(
                                tab.Text,
                                tab.Action,
                                tab.Controller,
                                tab.RouteValues,
                                new
                                {
                                    @title = tab.Text
                                }), first, tab.CssClass);
                }

                index++;
            }

            menuSb.Append("\n</ul>\n\n");
            return MvcHtmlString.Create(menuSb.ToString());

        }
        #endregion

        #region Tabbed Fw Menu

        public static MvcHtmlString TabbedIconMenu(this HtmlHelper helper, IEnumerable<MenuTab> tabs)
        {
            return TabbedIconMenu(helper, tabs, string.Empty);
        }

        public static MvcHtmlString TabbedIconMenu(this HtmlHelper helper, IEnumerable<MenuTab> tabs, string menuId)
        {
            return TabbedIconMenu(helper, tabs, menuId, false);
        }

        public static MvcHtmlString TabbedIconMenu(
            this HtmlHelper helper, IEnumerable<MenuTab> tabs, string menuId, bool singleController)
        {
            return TabbedIconMenu(helper, tabs, menuId, string.Empty, singleController);
        }


        public static MvcHtmlString TabbedIconMenu(this HtmlHelper helper, IEnumerable<MenuTab> tabs, string menuId,
            string menuCssClass, bool singleController)
        {
            var route = helper.ViewContext.IsChildAction
                            ? helper.ViewContext.ParentActionViewContext.RouteData
                            : helper.ViewContext.RouteData;

            var controller = route.GetRequiredString("controller");
            var action = route.GetRequiredString("action");
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            var ulTemplateFormat = "<ul id=\"{0}\" class=\"{1}\" >";
            var liTemplateFormat = "<li><a href=\"{0}\" class=\"{1}\"><i class=\"{2}\"></i>&nbsp;{3}</a>";

            var menuSb = new StringBuilder();
            menuSb.AppendFormat(ulTemplateFormat, menuId, menuCssClass);

            var index = 0;
            foreach (var tab in tabs)
            {
                var first = index <= 0 ? "first" : string.Empty;

                if (!singleController)
                {
                    if ((controller.ToLower() == tab.Controller.ToLower() && action.ToLower() == tab.Action.ToLower()) || controller.ToLower() == tab.Controller.ToLower())
                    {
                        menuSb.AppendFormat(
                           liTemplateFormat,
                           urlHelper.Action(
                                tab.Action.ToLowerInvariant(),
                                tab.Controller,
                                tab.RouteValues),
                                "active " + first,
                                tab.CssClass,
                                tab.Text);
                    }
                    else
                    {
                        menuSb.AppendFormat(
                             liTemplateFormat,
                             urlHelper.Action(
                                 tab.Action.ToLowerInvariant(),
                                 tab.Controller,
                                 tab.RouteValues),
                                 " " + first,
                                 tab.CssClass,
                                 tab.Text);
                    }
                }
                else
                {
                    if ((controller.ToLower() == tab.Controller.ToLower() && action.ToLower() == tab.Action.ToLower()))
                    {
                        menuSb.AppendFormat(
                            liTemplateFormat,
                            urlHelper.Action(
                                    tab.Action.ToLowerInvariant(),
                                    tab.Controller,
                                    tab.RouteValues),
                            "active " + first,
                            tab.CssClass,
                            tab.Text);
                    }
                    else
                    {
                        menuSb.AppendFormat(
                              liTemplateFormat,
                              urlHelper.Action(
                                      tab.Action.ToLowerInvariant(),
                                      tab.Controller,
                                      tab.RouteValues),
                              " " + first,
                              tab.CssClass,
                              tab.Text);
                    }
                }

                index++;
            }

            menuSb.Append("\n</ul>\n\n");
            return MvcHtmlString.Create(menuSb.ToString());

        }

        #endregion

    }
}