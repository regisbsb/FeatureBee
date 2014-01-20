namespace FeatureBee.Server.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    ///     ALlows persisting of a simple string collection.
    /// </summary>
    [ComplexType]
    public class PersistableStringCollection : PersistableScalarCollection<string>
    {
        public PersistableStringCollection()
        {
            
        }

        public PersistableStringCollection(IEnumerable<string> values)
        {
            foreach (var value in values)
            {
                Add(value);
            }
        }

        protected override string ConvertSingleValueToRuntime(string rawValue)
        {
            return rawValue;
        }

        protected override string ConvertSingleValueToPersistable(string value)
        {
            return value;
        }
    }
}