namespace FeatureBee.UpdateModes
{
    using System.Collections.Generic;

    using FeatureBee.UpdateModes.Receiving;
    using FeatureBee.WireUp;

    class Push : IFeatureRepository
    {
        private readonly string baseUrl;

        private List<FeatureDto> features = new List<FeatureDto>();

        private ICanReceiveFeatureStates itemHub;

        private ICanReceiveFeatureStates editHub;

        public Push(string baseUrl)
        {
            this.baseUrl = baseUrl;
            features = new Pull(baseUrl, withRefresh: false).PullFeatures();
            this.itemHub = new HubClient().Open(baseUrl, "boardHub")
                .On<string>("featureCreated", LoadUpdatedStatus)
                .On<string>("featureReleasedForEveryone", this.LoadUpdatedStatus)
                .On<string>("featureReleasedWithConditions", this.LoadUpdatedStatus)
                .On<string>("featureRollbacked", this.LoadUpdatedStatus);
            this.editHub = new HubClient().Open(baseUrl, "editPanelHub")
                .On<string>("descriptionUpdated", this.LoadUpdatedStatus)
                .On<string>("linkedToTicket", this.LoadUpdatedStatus)
                .On<string>("conditionsChanged", this.LoadUpdatedStatus);
        }

        private void LoadUpdatedStatus(string changedFeatureName)
        {
            features = new Pull(baseUrl, withRefresh: false).PullFeatures();
        }

        public List<FeatureDto> GetFeatures()
        {
            return features;
        }

        public void Dispose()
        {
            itemHub.Dispose();
            editHub.Dispose();
        }
    }
}
