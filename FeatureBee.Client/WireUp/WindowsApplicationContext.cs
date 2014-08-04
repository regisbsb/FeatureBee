namespace FeatureBee.WireUp
{
    using System.Collections.Generic;

    using FeatureBee.Conditions;

    internal class WindowsApplicationContext : IFeatureBeeContext
    {
        public WindowsApplicationContext()
        {
            GodModeFeatures = new GodModeFeatureCollection(); // Not supported, yet
            IsDebugMode = false; // Not supported, yet
        }

        public List<IConditionEvaluator> Evaluators { get; set; }
        public IFeatureRepository FeatureRepository { get; set; }
        public GodModeFeatureCollection GodModeFeatures { get; private set; }
        public bool IsDebugMode { get; private set; }
        public bool ShowTrayIconOnPages { get; set; }
        public string TrafficDistributionCookie { get; set; }
    }
}