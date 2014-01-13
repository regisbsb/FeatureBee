namespace FeatureBee.Server.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ConditionViewModel
    {
        [Key]
        public string Type { get; set; }
        public virtual List<ConditionValueViewModel> Values { get; set; }
        
        public void AddValue(string value)
        {
            var model = new ConditionValueViewModel(value);
            if (this.Values.Any(v => v.Value == model.Value)) return;
            this.Values.Add(model);
        }

        public void RemoveValue(string value)
        {
            if (this.Values.All(v => v.Value != value)) return;
            this.Values.Remove(this.Values.Single(_ => _.Value == value));
        }
    }
}