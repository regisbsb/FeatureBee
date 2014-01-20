namespace FeatureBee.ConfigSection
{
    using System.Configuration;

    internal class FeatureBeeConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("server", IsRequired = true)]
        public FeatureBeeServerConfig Server
        {
            get { return (FeatureBeeServerConfig) this["server"]; }
            set { this["server"] = value; }
        }

        [ConfigurationProperty("tray", IsRequired = false)]
        public FeatureBeeTrayConfig Tray
        {
            get { return (FeatureBeeTrayConfig) this["tray"]; }
            set { this["tray"] = value; }
        }

        [ConfigurationProperty("settings", IsRequired = false)]
        public FeatureBeeSettingsConfig Settings
        {
            get { return (FeatureBeeSettingsConfig)this["settings"]; }
            set { this["settings"] = value; }
        }

        public static FeatureBeeConfiguration GetSection()
        {
            return ConfigurationManager.GetSection("featureBee") as FeatureBeeConfiguration;
        }
    }
}