using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyData.Helpers
{
    /// <summary>
    /// Attribute helper class
    /// </summary>
    public class TableFieldNameAttribute : AttributeBase
    {
        /// <summary>
        /// Table field name attribute that matches db
        /// </summary>
        /// <param name="name"></param>
        public TableFieldNameAttribute(string name) : base(name) { }
    }
}
