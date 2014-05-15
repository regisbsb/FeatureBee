namespace FeatureBee.Server.Controllers
{
    using System.Collections.Generic;

    using FeatureBee.Server.Domain.ApplicationServices;
    using FeatureBee.Server.Domain.Infrastruture;
    using FeatureBee.Server.Domain.Models;

    using Machine.Fakes;
    using Machine.Specifications;

    public class BoardHubSpecs
    {
        [Subject(typeof(BoardHub), "Adding an item")]
        public class When_adding_an_item : WithSubject<BoardHub>
        {
            static CreateFeatureCommand command;

            Establish context = () => command = new CreateFeatureCommand("a", "b", "c", "e", new List<Condition>());

            Because of = () => Subject.AddNewItem(command);

            It should_dispatch_the_event = () => The<ICommandSender>().WasToldTo(x => x.Send(command));
        }

        [Subject(typeof(BoardHub), "Moving an item")]
        public class When_releasing_an_item_with_conditions : WithSubject<BoardHub>
        {
            Because of = () => Subject.MoveItem("a", 1);

            private It should_dispatch_the_event = () =>
                The<ICommandSender>().WasToldTo(x => x.Send(Param<ReleaseFeatureWithConditionsCommand>.Matches(_ => _.Name == "a")));
        }
    }
}