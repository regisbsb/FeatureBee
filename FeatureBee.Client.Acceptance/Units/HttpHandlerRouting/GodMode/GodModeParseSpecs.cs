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

namespace FeatureBee.Acceptance.Units.HttpHandlerRouting.GodMode
{
    class GodModeParseSpecs : WithSubject<GodModeFeatureStateEvaluator>
    {
        static GodModeFeatureCollection result;

        public class When_getting_null_value_to_parse
        {
            Because of = () => result = Subject.Parse(null);

            It should_return_one_result_of_type_GodModeFeatureCollection = () => result.Should().BeOfType<GodModeFeatureCollection>();

            It should_return_an_empty_GodModeFeatureCollection = () => result.Should().BeEmpty();
        }
        public class When_getting_correct_values_to_parse
        {
            Because of = () => result = Subject.Parse("#FeatureOne=true#FeatureTwo=false#");

            It should_return_the_parsed_values = () => result.Count.Should().Be(2);

            It should_return_featureOne = () => result.First().Key.Should().Be("FeatureOne");
            It should_return_featureOne_value = () => result.First().Value.Should().Be(true);
            It should_return_featureTwo = () => result.ElementAt(1).Key.Should().Be("FeatureTwo");
            It should_return_featureTwo_value = () => result.ElementAt(1).Value.Should().Be(false);
        }
        public class When_getting_weird_random_values_without_hashtag_to_parse
        {
            Because of = () => result = Subject.Parse("vhsdfkvbskdfvpiuköjbvdfköyjbzhpidfp");
            It should_return_another_GodModeFeatureCollection = () => result.Should().BeOfType<GodModeFeatureCollection>();
            It should_return_another_empty_GodModeFeatureCollection = () => result.Should().BeEmpty();
        }
        public class When_getting_weird_random_values_with_hashtag_to_parse
        {
            Because of = () => result = Subject.Parse("bla#bla");
            It should_return_another_GodModeFeatureCollection_hashtag = () => result.Should().BeOfType<GodModeFeatureCollection>();
            It should_return_another_empty_GodModeFeatureCollection_hashtag = () => result.Should().BeEmpty();
        }
       }
}
