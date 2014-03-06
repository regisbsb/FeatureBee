namespace FeatureBee.HttpHandlerRouting
{
    using System.Net;
    using System.Web;
    using System.Web.Script.Serialization;

    public class FeatureState : IHandleARoute
    {
        readonly JavaScriptSerializer serializer = new JavaScriptSerializer();

        public bool CanHandleRoute(string url)
        {
            return url.ToLowerInvariant().EndsWith("/feature/state");
        }

        public void DoHandleRoute(HttpContext context)
        {
            string featureName = context.Request.QueryString["name"];
            if (string.IsNullOrEmpty(featureName))
            {
                context.Response.Write(serializer.Serialize(new { success = false, message = "the query parameter for name is missing" }));
            }
            else
            {
                var isEnabled = Feature.IsEnabled(featureName);
                context.Response.Write(serializer.Serialize(new { success = true, isEnabled }));
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
        }
    }
}