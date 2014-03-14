using System;

namespace FeatureBee
{
    using System.Collections.Generic;
    using System.Linq;

    using FeatureBee.WireUp;

    public static class Feature
    {
        private static Func<string, bool> evaluator = new FeatureEvaluator().IsEnabled;

        public static void InjectEvaluator(Func<string, bool> isEnabled)
        {
            evaluator = isEnabled;
        }

        public static void RestoreDefaultEvaluator()
        {
            evaluator = new FeatureEvaluator().IsEnabled;
        }

        public static bool IsEnabled(string featureName)
        {
            return evaluator(featureName);
        }

        public static IEnumerable<string> EnabledFeatures()
        {
            var features = FeatureBeeBuilder.Context.FeatureRepository.GetFeatures();
            return features.Where(feature => IsEnabled(feature.Name)).Select(feature => feature.Name);
        }
    }
}
