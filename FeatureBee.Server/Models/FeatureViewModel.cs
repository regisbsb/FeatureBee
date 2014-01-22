namespace FeatureBee.Server.Models
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class FeatureViewModel
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual List<ConditionViewModel> Conditions { get; set; }
        public string State { get; set; }
        public int Index { get; set; }
        public string Link { get; set; }
        public string Team { get; set; }
        public string Description { get; set; }
    }
}