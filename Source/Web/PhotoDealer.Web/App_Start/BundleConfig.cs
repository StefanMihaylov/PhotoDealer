using System.Web.Optimization;

namespace PhotoDealer.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            RegisterScriptBundles(bundles);
            RegisterStylesBundles(bundles);

            BundleTable.EnableOptimizations = false;
        }

        private static void RegisterStylesBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.slate.css"));

            bundles.Add(new StyleBundle("~/Content/custom").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                        "~/Kendo/styles/kendo.common.min.css",
                        "~/Kendo/styles/kendo.common.core.min.css",
                        "~/Kendo/styles/kendo-bootstrap.common.min.css",
                        "~/Kendo/styles/kendo-bootstrap.common.core.min.css",
                        "~/Kendo/styles/kendo.black.min.css"));
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                // .Include("~/Scripts/jquery-{version}.js"));
                .Include("~/Scripts/Kendo/jquery.min.js")); // Kendo JQuery

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            // Kendo bundles
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                        "~/Kendo/kendo.web.min.js",
                        "~/Kendo/kendo.aspnetmvc.min.js"));
        }
    }
}
