namespace FeatureBee
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    using FeatureBee.WireUp;

    public class FeatureEvaluator
    {
        public bool IsEnabled(string featureName)
        {
            if (FeatureBeeBuilder.Context == null)
            {
                throw new InvalidOperationException("FeatureBeeBuilder.For[Web|WindowsService].Use[Config]() needs to be called first!");
            }

            var godModeFeatures = FeatureBeeBuilder.Context.GodModeFeatures;
            if (godModeFeatures.Any(x => x.Key.Equals(featureName, StringComparison.InvariantCultureIgnoreCase)))
            {
                var godMode = godModeFeatures.First(x => x.Key.Equals(featureName, StringComparison.InvariantCultureIgnoreCase));
                Logger.Log(TraceEventType.Verbose, "Feature {0} overwritten by GodMode. Value is {1}", featureName, godMode.Value);

                return godMode.Value;
            }

            var evaluators = FeatureBeeBuilder.Context.Evaluators;
            var features = FeatureBeeBuilder.Context.FeatureRepository.GetFeatures();

            var feature = features.FirstOrDefault(x => string.Equals(x.Name, featureName));

            if (feature == null)
            {
                Logger.Log(TraceEventType.Verbose, "Feature {0} does not exist", featureName);
                return false;
            }

            if (feature.State == "In Development" && !FeatureBeeBuilder.Context.IsDebugMode)
            {
                Logger.Log(TraceEventType.Verbose, "Feature {0} is 'In Development' and debug mode is false", feature.Name);
                return false;
            }

            if (feature.State == "Released")
            {
                Logger.Log(TraceEventType.Verbose, "Feature {0} is 'Released'", feature.Name);
                return true;
            }

            var isFulfilled = false;
            foreach (var condition in feature.Conditions)
            {
                var evaluator = evaluators.FirstOrDefault(x => string.Equals(x.Name, condition.Type));
                if (evaluator == null)
                {
                    return false;
                }

                isFulfilled = evaluator.IsFulfilled(condition.Values.ToArray());
                if (!isFulfilled)
                {
                    Logger.Log(TraceEventType.Verbose, "Feature {0} does not fulfill condition {1} of type {2}", feature.Name, condition.Values, condition.Type);
                    return false;
                }
            }

            return isFulfilled;
        }
    }
}
