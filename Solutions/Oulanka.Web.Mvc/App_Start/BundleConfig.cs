using System.Web.Optimization;

namespace Oulanka.Web.Mvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862

        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/scripts/jquery-{version}.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include(
                "~/scripts/jquery.validate.min.js",
                "~/scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                    "~/scripts/jquery.unobtrusive-ajax.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui")
                .Include("~/scripts/jquery-ui-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(
                new ScriptBundle("~/bundles/bootstrap")
                    .Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/plugins/bootbox/bootbox.min.js"));

            bundles.Add(new StyleBundle("~/css")
                .Include(
                        "~/Content/bootstrap.css",
                        "~/Content/font-awesome.css",
                        "~/Content/admin-lte/css/AdminLTE.css",
                        "~/Content/admin-lte/css/skins/skin-black.css",
                        "~/content/oulanka/validation.css"
                        ));

            bundles.Add(new StyleBundle("~/css/login")
                .Include(
                "~/Content/bootstrap.css",
                "~/Content/font-awesome.css",
                "~/Content/adminlte/css/adminlte.css",
                "~/content/oulanka/validation.css"
                ));

            bundles.Add(new StyleBundle("~/css/jqueryui").Include(
                "~/Content/themes/smoothness/jquery-ui.min.css"
                ));

            RegisterAppBundles(bundles);
        }

        private static void RegisterAppBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Scripts/plugins/slimScroll/jquery.slimscroll.min.js",
                        "~/Content/admin-lte/js/adminlte.js",
                        "~/Scripts/app/app.js"

                ));



        }

    }
}