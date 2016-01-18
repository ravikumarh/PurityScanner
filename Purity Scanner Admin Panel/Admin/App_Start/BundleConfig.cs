using System.Web;
using System.Web.Optimization;

namespace Admin
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/build-config.js"));
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/ckeditor.js"));
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/config.js"));
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/styles.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js", "~/build-config.js", "~/ckeditor.js", "~/config.js", "~/styles.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/js/jquery.cookie.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/bower_components/moment/min/moment.min.js"));
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/bower_components/fullcalendar/dist/fullcalendar.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/js/jquery.dataTables.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/bower_components/chosen/chosen.jquery.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/bower_components/colorbox/jquery.colorbox-min.js"));
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/js/jquery.noty.js"));
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/bower_components/responsive-tables/responsive-tables.js"));
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/bower_components/bootstrap-tour/build/js/bootstrap-tour.min.js"));
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/js/jquery.raty.min.js"));
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/js/jquery.iphone.toggle.js"));
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/js/jquery.autogrow-textarea.js"));
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/js/jquery.uploadify-3.1.min.js"));
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/js/jquery.history.js"));
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/js/charisma.js"));
        }
    }
}
