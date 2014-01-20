using System.Collections.Generic;

namespace FeatureBee.WireUp
{
    public class FeatureDto
    {
        public FeatureDto()
        {
            Conditions = new List<ConditionDto>();
        }

        public string Name { get; set; }
        public List<ConditionDto> Conditions { get; set; }
        public string State { get; set; }
    }
}