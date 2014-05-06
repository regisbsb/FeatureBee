namespace FeatureBee.Server.Controllers
{
    using System.Collections.Generic;

    using FeatureBee.Server.Domain.ApplicationServices;
    using FeatureBee.Server.Domain.Infrastruture;
    using FeatureBee.Server.Domain.Models;

    using Machine.Fakes;
    using Machine.Specifications;

    public class EditPanelHubSpecs
    {
        [Subject(typeof(EditPanelHub), "EditItem()")]
        public class When_editing_a_item : WithSubject<EditPanelHub>
        {
            static List<Condition> conditions;

            Establish context = () => conditions = new List<Condition>();

            Because of = () => Subject.EditItem(new EditItemDto("a", "b", "c", conditions));

            It should_dispatch_the_events = () =>
            {
                The<ICommandSender>().WasToldTo(x => x.Send(Param<UpdateDescriptionCommand>.Matches(_ => _.Name == "a" && _.Description == "b")));
                The<ICommandSender>().WasToldTo(x => x.Send(Param<LinkToTicketCommand>.Matches(_ => _.Name == "a" && _.Link == "c")));
                The<ICommandSender>().WasToldTo(x => x.Send(Param<UpdateConditionsCommand>.Matches(_ => _.Name == "a" && _.Conditions == conditions)));
            };
        }
    }
}