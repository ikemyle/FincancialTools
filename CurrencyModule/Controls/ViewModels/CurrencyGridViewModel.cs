using Currency.Core.Helpers;
using CurrencyConverter.Models;
using System.Collections.Generic;

namespace CurrencyConverter.Controls.ViewModels
{
    public class CurrencyGridViewModel : BaseBindable
    {
        private string[] MajorCurrencies = new string[] { "CAD", "CHF", "EUR", "GBP", "JPY", "USD" };
        public CurrencyGridViewModel()
        {
            SetCurrencyValues();
        }

        private List<CurrencySelectorItem> _currencies;
        public List<CurrencySelectorItem> Currencies
        {
            get { return _currencies; }

            set
            {
                SetProperty(ref _currencies, value);
            }
        }

        private CurrencyReporter _reportToCurrency;
        public CurrencyReporter ReportToCurrency
        {
            get { return _reportToCurrency; }
            set
            {
                SetProperty(ref _reportToCurrency, value);
                SetCurrencyValues();
            }
        }

        public void SetCurrencyValues()
        {
            List<CurrencySelectorItem> currencies = new List<CurrencySelectorItem>();
            foreach (var majorCurrencyIyem in MajorCurrencies)
            {
                var selectorItem = new CurrencySelectorItem(majorCurrencyIyem, this.ReportToCurrency);
                currencies.Add(selectorItem);
            }
            this.Currencies = currencies;
        }
    }
}
