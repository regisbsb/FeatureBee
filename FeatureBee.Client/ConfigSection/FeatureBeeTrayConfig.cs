namespace FeatureBee.ConfigSection
{
    using System.Configuration;

    internal class FeatureBeeTrayConfig : ConfigurationElement
    {
        [ConfigurationProperty("showTrayIconOnPages", DefaultValue = false, IsRequired = false)]
        public bool ShowTrayIconOnPages
        {
            get { return (bool) this["showTrayIconOnPages"]; }
            set { this["showTrayIconOnPages"] = value; }
        }

        [ConfigurationProperty("handlerPath", DefaultValue = "/featurebee.axd", IsRequired = false)]
        public string HandlerPath
        {
            get { return (string)this["handlerPath"]; }
            set { this["handlerPath"] = value; }
        }
    }
}