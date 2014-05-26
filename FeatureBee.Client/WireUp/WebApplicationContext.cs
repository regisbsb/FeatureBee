using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeatureBee.Evaluators;
using FeatureBee.GodMode;


namespace FeatureBee.WireUp
{
    internal class WebApplicationContext : IFeatureBeeContext
    {
        private readonly Func<HttpContextBase> _httpContextFunc;

        public WebApplicationContext(Func<HttpContextBase> httpContextFunc)
        {
            _httpContextFunc = httpContextFunc;
        }

        public List<IConditionEvaluator> Evaluators { get; set; }
        public IFeatureRepository FeatureRepository { get; set; }
        public bool ShowTrayIconOnPages { get; set; }
        public string TrafficDistributionCookie { get; set; }

        public bool IsDebugMode
        {
            get { return _httpContextFunc.Invoke().IsDebuggingEnabled; }
        }

        public GodModeFeatureCollection GodModeFeatures
        {
            get
            {
                var request = _httpContextFunc.Invoke().Request;
                var parser = new GodModeFeatureStateEvaluator();
                var strategies = new List<ICanGetGodModeStates>() { 
                    new GetGodModeFeaturesFromHttpHeader(parser), 
                    new GetGodModeFeaturesFromCookies(parser) 
                };

                var collection = new GodModeFeatureCollection();
                foreach (var strategy in strategies)
                {
                    collection.Combine(strategy.GetGodModeFeatures(request));
                }

                return collection;
            }
        }
    }
}