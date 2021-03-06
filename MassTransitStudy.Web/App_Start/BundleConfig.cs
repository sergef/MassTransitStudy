﻿namespace MassTransitStudy.Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new ScriptBundle("~/bundles/jquery")
                    .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/signalr")
                    .Include(
                        "~/Scripts/jquery.signalR-{version}.js",
                        "~/Content/Controllers/SampleMessagesHubController.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/jqueryval")
                    .Include("~/Scripts/jquery.validate*"));

            bundles.Add(
                new ScriptBundle("~/bundles/modernizr")
                    .Include("~/Scripts/modernizr-*"));

            bundles.Add(
                new ScriptBundle("~/bundles/bootstrap")
                    .Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/angularjs")
                    .Include("~/Scripts/angular.min.js"));

            bundles.Add(
                new StyleBundle("~/Content/css")
                    .Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // TODO: Set to true for production
            BundleTable.EnableOptimizations = false;
        }
    }
}
