namespace FeatureBee.UpdateModes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Runtime.Caching;
    using System.Web.Script.Serialization;

    using FeatureBee.WireUp;
    
    internal class Pull : IFeatureRepository
    {
        private static readonly ObjectCache Cache = new MemoryCache("FeatureBee");
        private readonly Uri featuresUri;
        private readonly WebClient webClient;

        private static bool disposing = false;

        public Pull(string url, bool withRefresh = true)
        {
            webClient = new WebClient();

            featuresUri = new Uri(string.Format("{0}/api/features", url));

            if (withRefresh)
            {
                RefreshCache(null);
            }
        }

        public List<FeatureDto> GetFeatures()
        {
            return Cache.Get("FeatureBee.Features") as List<FeatureDto> ?? new List<FeatureDto>();
        }

        private void RefreshCache(CacheEntryRemovedArguments arguments)
        {
            var expiresOn = DateTime.Now.AddSeconds(60);

            var features = PullFeatures();

            if (!disposing)
            {
                Cache.Set(new CacheItem("FeatureBee.Features", features),
                    new CacheItemPolicy { AbsoluteExpiration = expiresOn, RemovedCallback = RefreshCache });
            }
        }

        public List<FeatureDto> PullFeatures()
        {
            var features = new List<FeatureDto>();
            try
            {
                var result = webClient.DownloadString(featuresUri);
                features = Deserialize(result);

                Logger.Log(TraceEventType.Verbose, "Pulled features: " + result);
            }
            catch (Exception exception)
            {
                Logger.Log(TraceEventType.Error, "Failed to load features from url {0}. Exception: {1}", featuresUri, exception);
            }

            return features;
        }

        private List<FeatureDto> Deserialize(string input)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<List<FeatureDto>>(input);
        }

        public void Dispose()
        {
            Cache.Remove("FeatureBee.Features");
            disposing = true;
        }
    }
}