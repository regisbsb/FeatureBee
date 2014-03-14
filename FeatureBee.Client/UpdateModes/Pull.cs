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
        private readonly Uri _featuresUri;
        private readonly WebClient _webClient;

        private static bool disposing = false;

        public Pull(string url, bool withRefresh = true)
        {
            this._webClient = new WebClient();

            this._featuresUri = new Uri(string.Format("{0}/api/features", url));

            if (withRefresh)
            {
                this.RefreshCache(null);
            }
        }

        public List<FeatureDto> GetFeatures()
        {
            return Cache.Get("FeatureBee.Features") as List<FeatureDto> ?? new List<FeatureDto>();
        }

        private void RefreshCache(CacheEntryRemovedArguments arguments)
        {
            var expiresOn = DateTime.Now.AddSeconds(60);

            var features = this.PullFeatures();

            if (!disposing)
            {
                Cache.Set(new CacheItem("FeatureBee.Features", features),
                    new CacheItemPolicy { AbsoluteExpiration = expiresOn, RemovedCallback = this.RefreshCache });
            }
        }

        public List<FeatureDto> PullFeatures()
        {
            var features = new List<FeatureDto>();
            try
            {
                var result = this._webClient.DownloadString(this._featuresUri);
                features = this.Deserialize(result);
            }
            catch (Exception exception)
            {
                Logger.Log(TraceEventType.Error, "Failed to load features from url {0}. Exception: {1}", _featuresUri, exception);
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