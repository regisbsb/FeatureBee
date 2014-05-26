using FeatureBee.WireUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FeatureBee.GodMode
{
    public class GetGodModeFeaturesFromCookies : ICanGetGodModeStates
    {
        GodModeFeatureStateEvaluator parser;

        public GetGodModeFeaturesFromCookies(GodModeFeatureStateEvaluator parser)
        {
            this.parser = parser;
        }

        public GodModeFeatureCollection GetGodModeFeatures(HttpRequestBase request)
        {
            // are there cookies?
            if (request.Cookies == null)
            {
                // no = return empty collection
                return new GodModeFeatureCollection();
            }

            // do we have a cookie "FeatureBee"
            var cookie = request.Cookies["FeatureBee"];
            var value = cookie == null ? "" : HttpUtility.UrlDecode(cookie.Value) ?? "";

            return parser.Parse(value);
        }
    }
}
