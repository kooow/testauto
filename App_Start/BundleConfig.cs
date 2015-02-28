using System.Web;
using System.Web.Optimization;

namespace TestAuto
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            ScriptBundle new_scriptbundle = new ScriptBundle("~/bundles/jquerydatepicker");
            new_scriptbundle.Include("~/Scripts/jquery-ui.custom.js");
            new_scriptbundle.Include("~/Scripts/jquery.ui.*");

            new_scriptbundle.Include("~/Scripts/ui/jquery-ui.custom.js");
            new_scriptbundle.Include("~/Scripts/ui/jquery.ui.core.js");
            new_scriptbundle.Include("~/Scripts/ui/jquery.ui.datepicker.js");
            new_scriptbundle.Include("~/Scripts/ui/i18n/jquery.ui.datepicker-hu.js");

            bundles.Add(new_scriptbundle);

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            StyleBundle basic_css_files = new StyleBundle("~/Content/css");
            basic_css_files.Include("~/Content/site.css");
            basic_css_files.Include("~/Content/PagedList.css");

            bundles.Add(basic_css_files);


            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}