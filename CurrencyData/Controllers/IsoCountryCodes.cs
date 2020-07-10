using CurrencyData.Helpers;
using CurrencyData.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyData.Controllers
{
    /// <summary>
    /// Country codes class
    /// </summary>
    public class IsoCountryCodes
    {
        private static DBContext DataContext = new DBContext();
        /// <summary>
        /// Get all countries
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IsoCountryCodesModel> GetCountryCodes()
        {
            try
            {
                string sql = @"Select Country, IsoCode2, IsoCode3, NumericCode, [ISO-4217]
                            From ISOCountryCodes
                            Order by Country";
                var dt = DataContext.GetRecordSet(sql, CommandType.Text).Tables[0];
                var isoCountries = dt.ToEnumerable<IsoCountryCodesModel>();
                return isoCountries;
            }
            catch
            {
                throw;
            }
        }
    }

}
