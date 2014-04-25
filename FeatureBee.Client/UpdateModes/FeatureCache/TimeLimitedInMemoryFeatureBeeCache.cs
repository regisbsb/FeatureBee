namespace FeatureBee.UpdateModes.FeatureCache
{
    using System;
    using System.Collections.Generic;

    using FeatureBee.WireUp;

    public class TimeLimitedInMemoryFeatureBeeCache : TimeLimitedInMemoryCache<string, List<FeatureDto>>
    {
        public TimeLimitedInMemoryFeatureBeeCache(TimeSpan maxEntryAge)
            : base(maxEntryAge)
        {
        }

        public TimeLimitedInMemoryFeatureBeeCache(TimeSpan maxEntryAge, TimeSpan expiryInterval)
            : base(maxEntryAge, expiryInterval)
        {
        }
    }
}