namespace FeatureBee.Server.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Newtonsoft.Json;

    public class ConditionViewModel
    {

        public ConditionViewModel()
        {
            Id = Guid.NewGuid();
        }

        [JsonIgnore]
        [Key, ForeignKey("FeatureViewModel"), Column(Order = 1)]
        public Guid FeatureViewModelId { get; set; }

        [JsonIgnore]
        [Key, Column(Order = 2)]
        public Guid Id { get; set; }

        [JsonIgnore]
        public FeatureViewModel FeatureViewModel { get; set; }

        public string Type { get; set; }

        public virtual PersistableStringCollection Values { get; set; }
    }
}