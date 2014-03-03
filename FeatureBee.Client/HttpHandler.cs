using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using FeatureBee.WireUp;

namespace FeatureBee
{
    public class HttpHandler: IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Path.ToLowerInvariant().EndsWith("/features"))
            {
                context.Response.Write(GetFeaturesAsJson());
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
            }
        }

        private static string GetFeaturesAsJson()
        {
            var serializer = new JavaScriptSerializer();
            var features = FeatureBeeBuilder.Context.FeatureRepository.GetFeatures();
            return serializer.Serialize(features);
        }

        public bool IsReusable { get { return true; }}
    }
}
