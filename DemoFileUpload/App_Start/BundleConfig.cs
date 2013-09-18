using System.Web;
using System.Web.Optimization;

namespace DemoFileUpload {
    public class BundleConfig {
        // Para obtener más información acerca de Bundling, consulte http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/FileUpload").Include(
                        "~/FileUpload/tmpl.js",
                        "~/FileUpload/load-image.js",
                        "~/FileUpload/canvas-to-blob.js",
                        "~/Scripts/bootstrap.js",
                        "~/FileUpload/jquery.iframe-transport.js",
                        "~/FileUpload/jquery.fileupload.js",
                        "~/FileUpload/jquery.fileupload-process.js",
                        "~/FileUpload/jquery.fileupload-image.js",
                        "~/FileUpload/jquery.fileupload-audio.js",
                        "~/FileUpload/jquery.fileupload-video.js",
                        "~/FileUpload/jquery.fileupload-validate.js",
                        "~/FileUpload/jquery.fileupload-ui.js",
                        "~/FileUpload/main.js"
                        ));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de creación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/FileUpload/css").Include(
                "~/FileUpload/blueimp-gallery.css",
                "~/FileUpload/jquery.fileupload-ui.css"
            ));

            bundles.Add(new StyleBundle("~/FileUpload-noscript/css").Include(
                         "~/FileUpload/jquery.fileupload-ui-noscript.css"));

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