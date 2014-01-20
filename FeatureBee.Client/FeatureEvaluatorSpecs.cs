namespace FeatureBee
{
    using System.Collections.Generic;
    using System.Web;

    using FeatureBee.Evaluators;
    using FeatureBee.WireUp;

    using Machine.Fakes;
    using Machine.Specifications;

    public class FeatureEvaluatorSpecs : WithSubject<FeatureEvaluator>
    {
        Establish context = () =>
        {
            feature = new FeatureDto() { Name = featureName, State = "In Development", Conditions = new List<ConditionDto>() };
            featureRepo = An<IFeatureRepository>();
            featureRepo.WhenToldTo(_ => _.GetFeatures()).Return(new List<FeatureDto> { feature });
            var httpContextBase = An<HttpContextBase>();
            httpCookieCollection = new HttpCookieCollection();
            httpContextBase.WhenToldTo(_ => _.Request)
                .Return(
                    () =>
                    {
                        var httpResponseBase = An<HttpRequestBase>();
                        httpResponseBase.WhenToldTo(_ => _.Cookies).Return(httpCookieCollection);
                        return httpResponseBase;
                    });
            var conditionEvaluator = An<IConditionEvaluator>();
            conditionEvaluator.WhenToldTo(_ => _.Name).Return(condition);
            conditionEvaluator.WhenToldTo(_ => _.IsFulfilled(new [] { "always" })).Return(true);
            FeatureBeeBuilder.ForWebApp(() => httpContextBase).Use(featureRepo, new List<IConditionEvaluator> { conditionEvaluator });
        };

        private static IFeatureRepository featureRepo;
        
        public class WhenGodModeIsOn
        {
            Establish context = () => httpCookieCollection.Add(new HttpCookie("FeatureBee", string.Concat("#", featureName, "#")));

            Because of = () => featureState = Subject.IsEnabled(featureName);

            It should_not_released = () => featureState.ShouldBeTrue();

            private static bool featureState;
        }

        public class WhenFeatureIsInDevelopment
        {
            Establish context = () => feature.State = "In Development";

            Because of = () => featureState = Subject.IsEnabled(featureName);

            It should_not_be_released = () => featureState.ShouldBeFalse();

            private static bool featureState;
        }

        public class WhenFeatureIsUnderTestAndNoConditionMatches
        {
            Establish context = () =>
            {
                feature.State = "Under Test";
                feature.Conditions.Add(new ConditionDto { Type = condition, Values = new[] { "never" } });
            };

            Because of = () => featureState = Subject.IsEnabled(featureName);

            It should_not_be_released = () => featureState.ShouldBeFalse();

            private static bool featureState;
        }

        public class WhenFeatureIsUnderTestAndConditionMatches
        {
            Establish context = () =>
            {
                feature.State = "Under Test";
                feature.Conditions.Add(new ConditionDto { Type = condition, Values = new[] { "always" } });
            };

            Because of = () => featureState = Subject.IsEnabled(featureName);

            It should_not_released = () => featureState.ShouldBeTrue();

            private static bool featureState;
        }

        public class WhenFeatureIsReleased
        {
            Establish context = () => feature.State = "Released";

            Because of = () => featureState = Subject.IsEnabled(featureName);

            It should_be_released = () => featureState.ShouldBeTrue();

            private static bool featureState;
        }


        private static string featureName = "Lala";
        private static FeatureDto feature;
        private static string condition = "testcondition";

        private static HttpCookieCollection httpCookieCollection;
    }
}
