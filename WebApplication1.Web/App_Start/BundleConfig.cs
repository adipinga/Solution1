using System;
using System.Web.Optimization;

namespace WebApplication1.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include("~/Content/bootstrap.min.css",
                new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/style/css").Include("~/Content/style.css",
               new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/jquery/js").Include("~/Scripts/jquery-3.4.1.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include("~/Scripts/bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/validate/js").Include("~/Scripts/jquery.validate.unobtrusive.min.js"));
        }
    }
}