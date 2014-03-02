using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeatureBee.Evaluators;

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

                if (request.Cookies == null) 
                {
                    return new GodModeFeatureCollection();
                }

                var cookie = request.Cookies["FeatureBee"];
                var value = cookie == null ? "" : HttpUtility.UrlDecode(cookie.Value) ?? "";

                var godModeCollection = new GodModeFeatureCollection();
                value.Split('#').ToList().ForEach(
                    feature =>
                    {
                        var name = feature.Split('=').First();
                        var state = feature.Split('=').Last();
                        
                        // downwards compatibility
                        if (name == state)
                        {
                            state = "true";
                        }

                        bool stateAsBool;
                        bool.TryParse(state, out stateAsBool);
                        if (godModeCollection.ContainsKey(name))
                        {
                            godModeCollection[name] = stateAsBool;
                        }
                        else
                        {
                            godModeCollection.Add(name, stateAsBool);
                        }
                    });
                return godModeCollection;
            }
        }
    }
}