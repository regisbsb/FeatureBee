namespace FeatureBee.Server.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ConditionValueViewModel
    {
        public ConditionValueViewModel()
        {
            this.Id = Guid.NewGuid();
        }

        public ConditionValueViewModel(string value)
            : this()
        {
            this.Value = value;
        }

        [Key]
        public Guid Id { get; set; }

        public string Value { get; set; }
    }
}