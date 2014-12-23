using System.Web.Optimization;

namespace IsbnReader
{
    public static class BundleConfig
    {
        public static void Register(BundleCollection bundle)
        {
            bundle.Add(new ScriptBundle("~/js")
                .Include("~/Scripts/jquery*")
                .Include("~/Scripts/core.js")
                );

            bundle.Add(new StyleBundle("~/css")
                .Include("~/Content/bootstrap*")
                .Include("~/Content/Site*")
                );

            BundleTable.EnableOptimizations = true;
        }
    }
}