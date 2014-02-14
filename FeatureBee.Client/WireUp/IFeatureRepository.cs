namespace FeatureBee.WireUp
{
    using System;
    using System.Collections.Generic;

    public interface IFeatureRepository : IDisposable
    {
        List<FeatureDto> GetFeatures();
    }
}