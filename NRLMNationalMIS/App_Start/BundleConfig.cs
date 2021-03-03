using System.Web;
using System.Web.Optimization;

namespace NRLMNationalMIS
{
    public class BundleConfig
    {
       
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-3.0.0.js", "~/Scripts/jquery-ui-1.12.1.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/umd/popper.js", "~/Scripts/bootstrap.js", "~/Scripts/respond.js", "~/Scripts/bootbox.min.js", "~/Scripts/DataTables/jquery.dataTables.min.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/font-awesome.min.css", "~/Content/themes/base/jquery-ui.css", "~/Content/sass/main.css", "~/Content/DataTables/css/jquery.dataTables.min.css"));
            //BundleTable.EnableOptimizations = true;

        }
    }
}
