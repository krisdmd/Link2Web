using System.Web.Optimization;

namespace Link2Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-cerulean.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                        "~/Scripts/Telerik/kendo.all.min.js",
                        "~/Scripts/Telerik/kendo.aspnetmvc.min.js",
                        "~/Scripts/Telerik/kendo.timezones.min.js"));


            bundles.Add(new StyleBundle("~/Content/telerik").Include(
                        "~/Content/Telerik/kendo.common.min.css",
                        "~/Content/Telerik/kendo.rtl.min.css",
                        "~/Content/Telerik/kendo.default.min.css",
                        "~/Content/Telerik/kendo.default.mobile.min.css",
                        "~/Content/Telerik/kendo.mobile.all.min.css"));

            bundles.Add(new ScriptBundle("~/Content/telerikgrid").Include(
                        "~/Scripts/Telerik/kendo.grid.min.js"));
        }
    }
}
