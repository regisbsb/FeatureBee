namespace FeatureBee.Server.Models
{
    using System;

    using Newtonsoft.Json;

    public class FeatureHistoryViewModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string Payload { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
    }
}