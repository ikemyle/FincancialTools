using CurrencyData.Helpers;

namespace CurrencyData.Models
{
    /// <summary>
    /// Country codes model
    /// </summary>
    [TableFieldName("IsoCountryCodes")]
    public class IsoCountryCodesModel
    {
        /// <summary>
        /// Holding the country name
        /// </summary>
        [TableFieldName("Country")]
        public string Country { get; set; }
        /// <summary>
        /// Holding the two character Iso code
        /// </summary>
        [TableFieldName("IsoCode2")]
        public string Iso2 { get; set; }
        /// <summary>
        /// Holding the three character Iso code
        /// </summary>
        [TableFieldName("IsoCode3")]
        public string Iso3 { get; set; }
        /// <summary>
        /// Holding the numeric iso code for a country
        /// </summary>
        [TableFieldName("NumericCode")]
        public string NumericCode { get; set; }

        /// <summary>
        /// Holding the two character Iso currency code
        /// </summary>
        [TableFieldName("ISO-4217")]
        public string IsoCurrency { get; set; }
    }
}
