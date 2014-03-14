namespace FeatureBee.HttpHandlerRouting
{
    using System.Net;
    using System.Web;
    using System.Web.Script.Serialization;

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
            var features = Feature.AllFeatures();
            return serializer.Serialize(features);
        }
    }
}
