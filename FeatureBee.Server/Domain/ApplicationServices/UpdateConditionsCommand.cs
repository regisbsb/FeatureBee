namespace FeatureBee.Server.Domain.ApplicationServices
{
    using System.Collections.Generic;

    using FeatureBee.Server.Domain.Models;

    public class UpdateConditionsCommand : ICommand
    {
        public string Name { get; set; }
        public List<Condition> Conditions { get; set; }

        public UpdateConditionsCommand(string name, List<Condition> conditions)
        {
            Name = name;
            Conditions = conditions;
        }
    }
}