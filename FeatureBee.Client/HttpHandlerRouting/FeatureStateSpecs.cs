namespace FeatureBee.HttpHandlerRouting
{
    using Machine.Fakes;
    using Machine.Specifications;

    public class FeatureStateSpecs
    {
        public class When_Opening_The_state_For_A_Single_Features : WithSubject<FeatureState>
        {
            Because of = () => result = Subject.CanHandleRoute("/feature/state");

            It should_confirm_that_it_can_handle_the_route = () => result.ShouldBeTrue();

            private static bool result;
        }

        public class When_Opening_A_Different_Url : WithSubject<FeatureState>
        {
            Because of = () => result = Subject.CanHandleRoute("/different");

            It should_say_that_it_cannot_handle_the_route = () => result.ShouldBeFalse();

            private static bool result;
        }
    }
}
