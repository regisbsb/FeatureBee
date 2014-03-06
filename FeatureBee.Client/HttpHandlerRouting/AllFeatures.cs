namespace FeatureBee.HttpHandlerRouting
{
    using System.Net;
    using System.Web;
    using System.Web.Script.Serialization;

    using FeatureBee.WireUp;

    public class AllFeatures : IHandleARoute
    {
        public bool CanHandleRoute(string url)
        {
            return url.ToLowerInvariant().EndsWith("/features");
        }

        public void DoHandleRoute(HttpContext context)
        {
            context.Response.Write(GetFeaturesAsJson());
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
        }
        
        private static string GetFeaturesAsJson()
        {
            var serializer = new JavaScriptSerializer();
            var features = FeatureBeeBuilder.Context.FeatureRepository.GetFeatures();
            return serializer.Serialize(features);
        }
    }
}
