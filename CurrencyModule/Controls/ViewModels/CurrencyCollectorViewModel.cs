using Currency.Core.Helpers;
using CurrencyData.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyConverter.Controls.ViewModels
{
    public class CurrencyCollectorViewModel : BaseBindable
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public event EventHandler<CurrencySelectorItem> SelectionChanged;
        public CurrencyCollectorViewModel()
        {
            RefreshList();
            //this.Currencies = (new List<CurrencySelectorItem> { new CurrencySelectorItem("USD"), new CurrencySelectorItem("CHF"), 
            //    new CurrencySelectorItem("EUR"), new CurrencySelectorItem("GBP") }).OrderBy(x => x.Currency).ToList();

        }

        public void RefreshList()
        {
            var inUseCurrencies = IsoCurrencyCodes.AllCurrencies().Where(x => x.InUse == true).ToList();

            var currencyList = new List<CurrencySelectorItem>();
            foreach (var currencyUsed in inUseCurrencies)
            {
                currencyList.Add(new CurrencySelectorItem(currencyUsed.ISOCode));
            }
            this.Currencies = currencyList;

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

        private string _defaultSelection;
        public string DefaultSelection
        {
            get { return _defaultSelection; }
            set
            {
                _defaultSelection = value;
                var selected = this.Currencies.SingleOrDefault(x => x.Currency == value);
                if (selected != null) this.CurrencySelected = selected;
            }
        }

        private CurrencySelectorItem _currencySelected;
        public CurrencySelectorItem CurrencySelected
        {
            get { return _currencySelected; }
            set
            {
                var selected = Currencies.SingleOrDefault(x => x.Currency == value?.Currency);
                SetProperty(ref _currencySelected, selected);
                if (value!=null) value.ParentId = this.Id;
                if (SelectionChanged != null) SelectionChanged(this, value);
            }
        }
    }
}
