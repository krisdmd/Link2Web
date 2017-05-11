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
                "~/Scripts/bootstrap-dialog.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap-cerulean.css",
                "~/Content/bootstrap-dialog.css",
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                "~/Content/DataTables/css/dataTables.bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/telerik").Include(
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

            bundles.Add(new ScriptBundle("~/bundles/link2web").Include(
                "~/Scripts/custom-link2web.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                "~/Scripts/DataTables/jquery.dataTables.js"));

            bundles.Add(new StyleBundle("~/Content/custom").Include(
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/theme").Include(
                "~/Content/Theme/fonts/font-awesome.css",
                "~/Content/Theme/iCheck/skins/flat/green.css",
                "~/Content/Theme/bootstrap-progressbar-{version}.css",
                "~/Content/Theme/jqvmap.min.css",
                "~/Content/Theme/daterangepicker.css",
                "~/Content/Theme/custom.css"));

            bundles.Add(new ScriptBundle("~/bundles/theme").Include(
                "~/Scripts/Theme/smartresize.js",
                "~/Scripts/Theme/fastclick.js",
                "~/Scripts/Theme/gauge.js",
                "~/Scripts/Theme/bootstrap-progressbar.js",
                "~/Scripts/Theme/icheck.js",
                "~/Scripts/Theme/skycons.js",
                "~/Scripts/Theme/date.js",
                "~/Scripts/Theme/jquery.vmap.js",
                "~/Scripts/Theme/moment.js",
                "~/Scripts/Theme/daterangepicker.js",
                "~/Scripts/Theme/custom.js"));
        }
    }
}
