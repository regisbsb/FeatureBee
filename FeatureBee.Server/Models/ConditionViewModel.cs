namespace FeatureBee.Server.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ConditionViewModel
    {
        public ConditionViewModel()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid FeatureViewModelId { get; set; }

        public string Type { get; set; }

        public virtual List<ConditionValueViewModel> Values { get; set; }

        public void AddValue(string value)
        {
            var model = new ConditionValueViewModel(value);
            if (Values.Any(v => v.Value == model.Value)) return;
            Values.Add(model);
        }

        public void RemoveValue(string value)
        {
            if (Values.All(v => v.Value != value)) return;
            Values.Remove(Values.Single(_ => _.Value == value));
        }
    }
}