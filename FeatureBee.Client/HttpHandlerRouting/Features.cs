namespace FeatureBee.HttpHandlerRouting
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Script.Serialization;

    using FeatureBee.WireUp;

    public class Features : IHandleARoute
    {
        public bool CanHandleRoute(string url)
        {
            return url.ToLowerInvariant().EndsWith("/features");
        }

        public void DoHandleRoute(HttpContext context)
        {
            var featureName = context.Request.QueryString.GetValues("name");

            context.Response.Write(featureName == null ? GetFeaturesAsJson() : GetFeatureAsJson(featureName[0]));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
        }

        private string GetFeatureAsJson(string featureName)
        {
            var serializer = new JavaScriptSerializer();
            var feature = FeatureBeeBuilder.Context.FeatureRepository.GetFeatures().FirstOrDefault(x => x.Name.Equals(featureName, StringComparison.InvariantCultureIgnoreCase));

            if (feature == null)
            {
                return "";
            }

            return serializer.Serialize(new Feature
            {
                Name = feature.Name,
                State = feature.State,
                Team = feature.Team,
                Enabled = Feature.IsEnabled(feature.Name)
            });

        }

        private static string GetFeaturesAsJson()
        {
            var serializer = new JavaScriptSerializer();
            var features = Feature.AllFeatures();
            return serializer.Serialize(features);
        }
    }
}
