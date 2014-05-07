namespace FeatureBee.Acceptance.Units.HttpHandlerRouting
{
    using FeatureBee.HttpHandlerRouting;

    using FluentAssertions;

    using Machine.Fakes;
    using Machine.Specifications;

    public class FeaturesSpecs
    {
        [Subject(typeof(Features), "CanHandleRoute()")]
        public class When_Opening_The_Url_For_All_Features : WithSubject<Features>
        {
            Because of = () => result = Subject.CanHandleRoute("/features");

            It should_confirm_that_it_can_handle_the_route = () => result.Should().BeTrue();

            private static bool result;
        }

        [Subject(typeof(Features), "CanHandleRoute()")]
        public class When_Opening_A_Different_Url : WithSubject<Features>
        {
            Because of = () => result = Subject.CanHandleRoute("/different");

            It should_say_that_it_cannot_handle_the_route = () => result.Should().BeFalse();

            private static bool result;
        }
    }
}