namespace FeatureBee.Server.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    /// <summary>
    ///     Baseclass that allows persisting of scalar values as a collection (which is not supported by EF 4.3)
    /// </summary>
    /// <typeparam name="T">Type of the single collection entry that should be persisted.</typeparam>
    /// <seealso
    ///     cref="http://stackoverflow.com/questions/11985267/entity-framework-options-to-map-list-of-strings-or-list-of-int-liststring" />
    [ComplexType]
    public abstract class PersistableScalarCollection<T> : ICollection<T>
    {
        // use a character that will not occur in the collection.
        // this can be overriden using the given abstract methods (e.g. for list of strings).
        private const string DefaultValueSeperator = "|";

        private readonly string[] defaultValueSeperators = {DefaultValueSeperator};

        public PersistableScalarCollection()
        {
            Data = new List<T>();
        }

        /// <summary>
        ///     The internal data container for the list data.
        /// </summary>
        private List<T> Data { get; set; }

        /// <summary>
        ///     Deriving classes can override the string that is used to seperate single values
        /// </summary>
        protected virtual string ValueSeperator
        {
            get { return DefaultValueSeperator; }
        }

        /// <summary>
        ///     Deriving classes can override the string that is used to seperate single values
        /// </summary>
        protected virtual string[] ValueSeperators
        {
            get { return defaultValueSeperators; }
        }

        /// <summary>
        ///     DO NOT Modeify manually! This is only used to store/load the data.
        /// </summary>
        public string SerializedValue
        {
            get
            {
                var serializedValue = string.Join(ValueSeperator,
                    Data.Select(x => ConvertSingleValueToPersistable(x))
                        .ToArray());
                return serializedValue;
            }
            set
            {
                Data.Clear();

                if (string.IsNullOrEmpty(value))
                {
                    return;
                }

                Data = new List<T>(value.Split(ValueSeperators, StringSplitOptions.None)
                    .Select(x => ConvertSingleValueToRuntime(x)));
            }
        }

        public void Add(T item)
        {
            Data.Add(item);
        }

        public void Clear()
        {
            Data.Clear();
        }

        public bool Contains(T item)
        {
            return Data.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Data.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Data.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return Data.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        /// <summary>
        ///     Implementors have to convert the given value raw value to the correct runtime-type.
        /// </summary>
        /// <param name="rawValue">the already seperated raw value from the database</param>
        /// <returns></returns>
        protected abstract T ConvertSingleValueToRuntime(string rawValue);

        /// <summary>
        ///     Implementors should convert the given runtime value to a persistable form.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected abstract string ConvertSingleValueToPersistable(T value);
    }
}