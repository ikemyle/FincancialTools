using System;

namespace CurrencyData.Helpers
{
    /// <summary>
    /// Helper type extension class
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// Method to check a enumerable type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNonStringEnumerable(this Type type)
        {
            if (type == null || type == typeof(string))
                return false;
            return typeof(System.Collections.IEnumerable).IsAssignableFrom(type);
        }
    }
}
