namespace FeatureBee.ConfigSection
{
    using System.Configuration;

    internal class FeatureBeeSettingsConfig : ConfigurationElement
    {
        [ConfigurationProperty("trafficDistributionCookie", DefaultValue = "fbee", IsRequired = false)]
        public string TrafficDistributionCookie
        {
            get { return (string) this["trafficDistributionCookie"]; }
            set { this["trafficDistributionCookie"] = value; }
        }
    }
}