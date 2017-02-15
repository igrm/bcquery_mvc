using System.Configuration;
using System.Web;
using System.Web.Optimization;

namespace bcquery_mvc
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/angular/all.js").Include("~/Scripts/angular/angular.min.js"
            , "~/Scripts/angular-animate/angular-animate.min.js"
            , "~/Scripts/angular-aria/angular-aria.min.js"
            , "~/Scripts/angular-material/angular-material.min.js"
            , "~/Scripts/angular-resource/angular-resource.min.js"
            , "~/Scripts/angular-data-table/md-data-table.min.js")
            );

            bundles.Add(new ScriptBundle("~/bundles/application/all.js").Include("~/Scripts/application/app.js"));

            bundles.Add(new StyleBundle("~/Content/css/all.css")
                .Include("~/Content/angular-material.min.css", "~/Content/md-data-table.min.css"));
        }
    }
}