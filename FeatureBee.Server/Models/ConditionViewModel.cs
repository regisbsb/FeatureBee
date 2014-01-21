namespace FeatureBee.Server.Models
{
    using System;

    public class ConditionViewModel
    {
        public ConditionViewModel()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid FeatureViewModelId { get; set; }

        public string Type { get; set; }

        public virtual PersistableStringCollection Values { get; set; }
    }
}