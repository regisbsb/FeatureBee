namespace FeatureBee.Server.Domain.ApplicationServices
{
    public class DeleteFeatureCommand : ICommand
    {
        public DeleteFeatureCommand(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}