using FeatureBee.WireUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;



namespace FeatureBee.GodMode
{
   public class GetGodModeFeaturesFromHttpHeader : ICanGetGodModeStates
    {
        GodModeFeatureStateEvaluator parser;

        public GetGodModeFeaturesFromHttpHeader(GodModeFeatureStateEvaluator parser)
        {
            this.parser = parser;
        }

        public GodModeFeatureCollection GetGodModeFeatures(HttpRequestBase request)
        {
            if (request.Headers == null || (request.Headers["X-FeatureBee-Http"]) == null)
            {
                return new GodModeFeatureCollection();
            }
            var valueHeader = request.Headers["X-FeatureBee-Http"];

            return parser.Parse(valueHeader);
        }
    }
}
