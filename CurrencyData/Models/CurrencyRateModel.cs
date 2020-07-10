using CurrencyData.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyData.Models
{
    /// <summary>
    /// currency data definition
    /// </summary>
    [TableFieldName("CurrencyRates")]
    public class CurrencyRateModel
    {
        public CurrencyRateModel(string currencyBase,DateTime RetrievedDate)
        {
            this.Base = currencyBase;
            this.RateDate = RetrievedDate;
        }
        /// <summary>
        /// Currency name
        /// </summary>
        [TableFieldName("Currency")]
        public string Currency { get; set; }
        /// <summary>
        /// The date of the rate is eretrieved
        /// </summary>
        [TableFieldName("RateDate")]
        public DateTime RateDate { get; set; }
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
    }
}
