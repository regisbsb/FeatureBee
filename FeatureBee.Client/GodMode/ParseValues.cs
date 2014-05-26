using FeatureBee.WireUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureBee.GodMode
{
    public class GodModeFeatureStateEvaluator
    {
        public virtual GodModeFeatureCollection Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                return new GodModeFeatureCollection();
            if (!value.Contains("#")) return new GodModeFeatureCollection();
            if (value.Contains("#") && !value.Contains("=")) return new GodModeFeatureCollection();

            var godModeCollection = new GodModeFeatureCollection();
            value.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(
                feature =>
                {
                    var name = feature.Split('=').First();
                    var state = feature.Split('=').Last();

                    bool stateAsBool;
                    if (!bool.TryParse(state, out stateAsBool)) return;
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
