namespace FeatureBee.WireUp
{
    using System.Collections.Generic;

    public class ConditionDto
    {
        public ConditionDto()
        {
            Values = new List<string>();
        }

        public string Type { get; set; }
        public List<string> Values { get; set; }
    }
}