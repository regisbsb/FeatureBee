namespace FeatureBee.Server.Domain.ApplicationServices
{
    public class RemoveValueFromConditionCommand : ICommand
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string[] Values { get; set; }

        public RemoveValueFromConditionCommand(string name, string type, string[] values)
        {
            Name = name;
            Type = type;
            Values = values;
        }
    }
}