using FeatureBee.GodMode;
using FeatureBee.WireUp;
using FluentAssertions;
using Machine.Fakes;
using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FeatureBee.Acceptance.Units.HttpHandlerRouting.GodMode
{
    public class GodModeFromHttpHeaderSpecs : WithSubject<GetGodModeFeaturesFromHttpHeader>
    {
        static HttpRequestBase requestBase;
        static GodModeFeatureCollection result;

        Establish context = () =>
        {
            The<GodModeFeatureStateEvaluator>()
                .WhenToldTo(_ => _.Parse(Param<string>.IsAnything))
                .Return(new GodModeFeatureCollection());
            requestBase = An<HttpRequestBase>();
            requestBase.WhenToldTo(_ => _.Headers).Return((NameValueCollection)null);
        };

        public class When_getting_god_mode_features_but_no_header_value_set
        {
            Because of = () => result = Subject.GetGodModeFeatures(requestBase);

            It should_return_no_result = () => result.Should().BeEmpty();
        }
        
        public class When_getting_god_mode_feature_with_a_featureBee_Header_and_value
        {
            Establish context = () =>
            {               
                var collection = new GodModeFeatureCollection();
                collection.Add("feature", true);

                var header = new NameValueCollection();
                header.Add("X-FeatureBee-Http", "blablablub");

                The<GodModeFeatureStateEvaluator>()
                    .WhenToldTo(_ => _.Parse("blablablub"))
                    .Return(collection);
                requestBase.WhenToldTo(_ => _.Headers).Return((NameValueCollection)header);
            };

            Because of = () => result = Subject.GetGodModeFeatures(requestBase);

            It should_return_one_result = () => result.Count.Should().Be(1);

            It should_have_parsed_the_value = () => The<GodModeFeatureStateEvaluator>().WasToldTo(_ => _.Parse("blablablub")).OnlyOnce();
        }
    }
}
