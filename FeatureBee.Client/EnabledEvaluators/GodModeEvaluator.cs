namespace FeatureBee.EnabledEvaluators
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    using FeatureBee.WireUp;

    class GodModeEvaluator : IEvaluateFeatures
    {
        private readonly GodModeFeatureCollection godModeFeatures;

        public GodModeEvaluator()
        {
            this.godModeFeatures = FeatureBeeBuilder.Context.GodModeFeatures;
        }

        public bool CanEvalute(string featureName, FeatureDto feature)
        {
            return this.godModeFeatures.Any(x => x.Key.Equals(featureName, StringComparison.InvariantCultureIgnoreCase));
        }

        public bool IsEnabled(string featureName, FeatureDto feature)
        {
            var godMode = this.godModeFeatures.First(x => x.Key.Equals(featureName, StringComparison.InvariantCultureIgnoreCase));
            Logger.Log(TraceEventType.Verbose, "Feature {0} overwritten by GodMode. Value is {1}", featureName, godMode.Value);

            return godMode.Value;
        }
    }
}