using System.Collections.Generic;
using System.Linq;
using Currency.Core.Model;
using CurrencyData.Controllers;
using CurrencyData.Models;

namespace Currency.Core.Services
{
    public class CountryService : ICountryService
    {
        public List<IsoCountryCodesModel> GetAllCountries()
        {
            var countries = new List<IsoCountryCodesModel>();

            countries = IsoCountryCodes.GetCountryCodes().ToList();
            return countries;
        }
    }
}
