namespace FeatureBee.HttpHandlerRouting
{
    using Machine.Fakes;
    using Machine.Specifications;

    public class AllFeaturesSpecs
    {
        public class When_Opening_The_Url_For_All_Features : WithSubject<AllFeatures>
        {
            Because of = () => result = Subject.CanHandleRoute("/features");

            It should_confirm_that_it_can_handle_the_route = () => result.ShouldBeTrue();

            private static bool result;
        }

        public class When_Opening_A_Different_Url : WithSubject<AllFeatures>
        {
            Because of = () => result = Subject.CanHandleRoute("/different");

            It should_say_that_it_cannot_handle_the_route = () => result.ShouldBeFalse();

            private static bool result;
        }
    }
}
