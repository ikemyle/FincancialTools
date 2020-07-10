using System.Collections.Generic;
using Currency.Core.Model;
using CurrencyData.Models;

namespace Currency.Core.Services
{
    public interface ICountryService
    {
        List<IsoCountryCodesModel> GetAllCountries();
    }
}
