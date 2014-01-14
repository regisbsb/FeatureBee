namespace FeatureBee.Server.ConfigSection
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    [ConfigurationCollection(typeof(ConfigurationElement))]
    internal class ConfigurationElementCollection<T> : ConfigurationElementCollection where T : ConfigurationElement, new()
    {
        private bool readonlyCollection;

        public T this[int idx]
        {
            get { return (T)this.BaseGet(idx); }
        }

        public T this[object key]
        {
            get
            {
                return (T)this.BaseGet(key);
            }
        }

        public override bool IsReadOnly()
        {
            return this.readonlyCollection;
        }

        public void MakeReadOnly()
        {
            this.readonlyCollection = true;
        }

        public void MakeWriteable()
        {
            this.readonlyCollection = false;
        }

        public void RemoveElement(T element)
        {
            int index = this.BaseGetAllKeys().TakeWhile(key => key.ToString() != this.GetElementKey(element).ToString()).Count();
            if (index >= this.Count)
            {
                return;
            }

            this.BaseRemoveAt(index);
        }

        public void AddElement(T element)
        {
            this.BaseAdd(element);
        }

        public void AddElements(ConfigurationElementCollection<T> elements)
        {
            foreach (T element in elements)
            {
                this.AddElement(element);
            }
        }

        public bool HasKey(object key)
        {
            return this.BaseGetAllKeys().Any(k => k.ToString() == key.ToString());
        }

        public T[] ToArray()
        {
            return this.BaseGetAllKeys().Select(k => this[k]).ToArray();
        }

        public List<T> ToList()
        {
            return this.BaseGetAllKeys().Select(k => this[k]).ToList();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var key = new StringBuilder();
            foreach (PropertyInfo property in element.GetType().GetProperties())
            {
                var attributes =
                    (ConfigurationPropertyAttribute[])property.GetCustomAttributes(typeof(ConfigurationPropertyAttribute), true);
                if (attributes.Length < 1)
                {
                    continue;
                }

                key.Append(this.CheckAttributes(element, property, attributes));
            }

            return string.IsNullOrEmpty(key.ToString()) ? element.ToString() : key.ToString();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new T();
        }

        private string CheckAttributes(ConfigurationElement element, PropertyInfo property, IEnumerable<ConfigurationPropertyAttribute> attributes)
        {
            return attributes.Any(attribute => attribute.IsKey) ? property.GetValue(element, null).ToString() : string.Empty;
        }
    }
}