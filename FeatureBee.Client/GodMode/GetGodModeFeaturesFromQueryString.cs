using FeatureBee.WireUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FeatureBee.GodMode
{
    public class GetGodModeFeaturesFromQueryString :ICanGetGodModeStates
    {
        GodModeFeatureStateEvaluator parser;

        public GetGodModeFeaturesFromQueryString(GodModeFeatureStateEvaluator parser)
        {
            this.parser = parser;
        }

        public GodModeFeatureCollection GetGodModeFeatures(HttpRequestBase request)
        {
            if (request.QueryString == null || (request.QueryString["FeatureBee"]) == null)
            {
                return new GodModeFeatureCollection();
            }
            var valueHeader = request.QueryString["FeatureBee"];

            return parser.Parse(valueHeader);
        }
    }
}
