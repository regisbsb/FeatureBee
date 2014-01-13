namespace FeatureBee.Server.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FeatureViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual List<ConditionViewModel> Conditions { get; set; }
        public string State { get; set; }
        public int Index { get; set; }
        public string Link { get; set; }
        public string Team { get; set; }
        public string Description { get; set; }

        public void AddCondition(string type)
        {
            if (Conditions == null) Conditions = new List<ConditionViewModel>();
            if (Conditions.Any(_ => _.Type == type)) return;
            Conditions.Add(new ConditionViewModel() { Type = type, Values = new List<ConditionValueViewModel>() });
        }
    }
}