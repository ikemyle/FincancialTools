using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CurrencyData.Helpers
{
    /// <summary>
    /// Conversion class from datatable
    /// </summary>
    public static class DataConversion
    {
        /// <summary>
        /// Generic method of conversion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ICollection<T> ToEnumerable<T>(this DataTable source) where T : new()
        {
            if (source == null) return null;

            // Init objects
            List<T> items = new List<T>();
            TableFieldNameAttribute attribute = null;
            var properties = typeof(T).GetProperties();

            T item;
            foreach (DataRow row in source.Rows)
            {
                // Create a new instance of the desired object.
                item = (T)Activator.CreateInstance(typeof(T));
                foreach (var property in properties)
                {
                    // Get the TableFieldName attribute.
                    attribute = property
                        .GetCustomAttributes(typeof(TableFieldNameAttribute), true)
                        .FirstOrDefault() as TableFieldNameAttribute;

                    // If there is no attribute, we need to skip the property.
                    if (attribute == null) continue;

                    if (source.Columns.Contains(attribute.Name))
                    {
                        if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType)
                            && property.PropertyType.IsNonStringEnumerable())
                        {
                            // TODO : Handle IEnumerable object. 
                            // If you want to help, please make a pull request,
                            // I will reference you in my article also. :)
                        }
                        else
                        {
                            // Set the desired object with the value from the row.
                            if (property.PropertyType == typeof(decimal)
                                && row[attribute.Name].ToString() == string.Empty)
                            {
                                property.SetValue(item, Convert.ChangeType(0
                                    , property.PropertyType), null);
                            }
                            else
                            {
                                property.SetValue(item, Convert.ChangeType(row[attribute.Name]
                                    , property.PropertyType), null);
                            }
                        }
                    }
                }
                items.Add(item);
            }

            return items;
        }
    }
}
