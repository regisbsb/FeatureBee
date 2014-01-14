namespace FeatureBee.Server.ConfigSection
{
    using System.Configuration;

    internal class FeatureBeeConfiguration : ConfigurationSection
    {
         [ConfigurationProperty("teams", IsRequired = true)]
         [ConfigurationCollection(typeof(Team), AddItemName = "add")]
         public ConfigurationElementCollection<Team> Teams
         {
             get { return (ConfigurationElementCollection<Team>)this["teams"]; }
         }

        public static FeatureBeeConfiguration GetSection()
        {
            return ConfigurationManager.GetSection("featureBee") as FeatureBeeConfiguration;
        }
    }

    internal class Team : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
    }
}