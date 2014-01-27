namespace FeatureBee.Server.Controllers
{
    using System.Collections.Generic;

    using FeatureBee.Server.Domain.ApplicationServices;
    using FeatureBee.Server.Domain.Infrastruture;
    using FeatureBee.Server.Domain.Models;

    using Microsoft.AspNet.SignalR;

    public class EditPanelHub : Hub
    {
        private readonly ICommandSender commandSender;

        public EditPanelHub(ICommandSender commandSender)
        {
            this.commandSender = commandSender;
        }

        public void EditItem(EditItemDto changes)
        {
            commandSender.Send(new UpdateDescriptionCommand(changes.Name, changes.Description));
            commandSender.Send(new LinkToTicketCommand(changes.Name, changes.Link));
            commandSender.Send(new UpdateConditionsCommand(changes.Name, changes.Conditions));
        }
    }

    public class EditItemDto
    {
        public EditItemDto(string name, string description, string link, List<Condition> conditions)
        {
            Link = link;
            Conditions = conditions;
            Description = description;
            Name = name;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Link { get; private set; }
        public List<Condition> Conditions { get; private set; }
    }
}