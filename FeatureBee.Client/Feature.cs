using System;

namespace FeatureBee
{
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
    }
}
