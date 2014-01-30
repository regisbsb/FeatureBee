namespace FeatureBee.Server.Domain.Models
{
    using System.Collections.Generic;

    public class Condition
    {
        public Condition()
        {
            Values = new List<string>();
        }

        public string Type { get; set; }

        public List<string> Values { get; set; }
    }
}