
using CurrencyData.Helpers;
using System;

namespace CurrencyData.Models
{
    /// <summary>
    /// currency data definition
    /// </summary>
    [TableFieldName("ISOCurrencies")]
    public class IsoCurrencyModel : ICloneable
    {
        public IsoCurrencyModel()
        {

        }

        public IsoCurrencyModel(string CurrencyCode)
        {
            ISOCode = CurrencyCode;
        }
        /// <summary>
        /// ISO Currency code
        /// </summary>
        [TableFieldName("ISO-4217")]
        public string ISOCode { get; set; }
        /// <summary>
        /// Currency long name
        /// </summary>
        [TableFieldName("Currency")]
        public string Currency { get; set; }
        /// <summary>
        /// Flag that shows currency is in use by user
        /// </summary>
        [TableFieldName("InUse")]
        public bool InUse { get; set; }
        /// <summary>
        /// Conversion rate reported to base
        /// </summary>
        [TableFieldName("Rate")]
        public double Rate { get; set; }
        /// <summary>
        /// Currency base to report to
        /// </summary>
        [TableFieldName("Base")]
        public string Base { get; set; }
        /// <summary>
        /// Currency base to report to
        /// </summary>
        [TableFieldName("Symbol")]
        public string Symbol { get; set; }
        public object Clone()
        {
            return new IsoCurrencyModel { ISOCode = this.ISOCode, Currency = this.Currency, Rate = this.Rate, Base = this.Base };
        }
    }
}
