using FeatureBee.GodMode;
using FeatureBee.WireUp;

    using FluentAssertions;

using Machine.Fakes;
using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FeatureBee.Acceptance.Units.HttpHandlerRouting.GodMode
{
    public class GodModeFromHttpCookieSpecs : WithSubject<GetGodModeFeaturesFromCookies>
    {
        Establish context = () =>
        {
            The<GodModeFeatureStateEvaluator>()
                .WhenToldTo(_ => _.Parse(Param<string>.IsAnything))
                .Return(new GodModeFeatureCollection());
            requestBase = An<HttpRequestBase>();
            requestBase.WhenToldTo(_ => _.Cookies).Return((HttpCookieCollection)null);
        };

        public class When_getting_god_mode_features_but_no_cookie_set
        {
            Because of = () => result = Subject.GetGodModeFeatures(requestBase);

            It should_return_no_result = () => result.Should().BeEmpty();
        }

        public class When_getting_god_mode_feature_with_a_cookie_but_not_a_featureBee_one
        {
            Establish context = () =>
            {
                var cookies = new HttpCookieCollection();
                cookies.Add(new HttpCookie("otherCookie"));
                requestBase.WhenToldTo(_ => _.Cookies).Return(cookies);
            };

            Because of = () => result = Subject.GetGodModeFeatures(requestBase);

            It should_return_no_result = () => result.Should().BeEmpty();
        }

        public class When_getting_god_mode_feature_with_a_featureBee_cookie_and_a_value
        {
            Establish context = () =>
            {
                var cookies = new HttpCookieCollection();
                cookies.Add(new HttpCookie("FeatureBee", "blabla"));
                var collection = new GodModeFeatureCollection();
                collection.Add("feature", true);

                The<GodModeFeatureStateEvaluator>()
                    .WhenToldTo(_ => _.Parse("blabla"))
                    .Return(collection);
                requestBase.WhenToldTo(_ => _.Cookies).Return(cookies);
            };

            Because of = () => result = Subject.GetGodModeFeatures(requestBase);

            It should_return_one_result = () => result.Count.Should().Be(1);

            It should_have_parsed_the_value = () => The<GodModeFeatureStateEvaluator>().WasToldTo(_ => _.Parse("blabla")).OnlyOnce();
        }

        static HttpRequestBase requestBase;
        static GodModeFeatureCollection result;
    }
}
