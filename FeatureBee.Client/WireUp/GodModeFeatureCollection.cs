namespace FeatureBee.WireUp
{
    using System.Collections.Generic;
    
    public class GodModeFeatureCollection : Dictionary<string, bool>
    {
        public GodModeFeatureCollection Combine(GodModeFeatureCollection with)
        {
            foreach (var key in with.Keys)
            {
                if (!ContainsKey(key)) Add(key, with[key]);
            }

            return this;
        }
    }
}