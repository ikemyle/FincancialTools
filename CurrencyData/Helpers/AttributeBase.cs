using System;

namespace CurrencyData.Helpers
{
    /// <summary>
    /// Attribute helper class
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class AttributeBase : Attribute
    {

        private string _name = string.Empty;
        /// <summary>
        /// Attribute name
        /// </summary>
        /// <param name="name"></param>
        public AttributeBase(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Attribute name that will be overriden
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
        }

    }
}
