using Currency.Core.Helpers;
using Currency.Core.Services;
using CurrencyConverter.Controls;
using CurrencyConverter.Controls.ViewModels;
using CurrencyConverter.Helpers;
using CurrencyConverter.Models;
using CurrencyData.Controllers;
using CurrencyData.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace CurrencyConverter.ViewModels
{
    public class CurrencyConversionViewModel : BaseBindable
    {
        private string FromSelector = "FROMSEL";
        private string ToSelector = "TOSEL";
        public CurrencyCollectorViewModel SelectorFrom = new CurrencyCollectorViewModel();
        public CurrencyCollectorViewModel SelectorTo = new CurrencyCollectorViewModel();
        public CurrencyInputViewModel CurrencyInput = new CurrencyInputViewModel();
        public CurrencyInputViewModel CurrencyOutput = new CurrencyInputViewModel();
        public CurrencyGridViewModel CurrencyGrid = new CurrencyGridViewModel();

        private void SetCurrencies()
        {
            var inCurrency = this.SelectorFrom.CurrencySelected?.Currency;
            var outCurrency = this.SelectorTo.CurrencySelected?.Currency;

            this.CurrencyInput.CurrencySymbol = inCurrency;
            this.CurrencyOutput.CurrencySymbol = outCurrency;

            this.FromCurrency = new CurrencySelectorItem(inCurrency);

            var currencyModelInpout = CurrencyInfo(inCurrency);
            var currencyModelOutpout = CurrencyInfo(outCurrency);

            this.CurrencyInput.Description = currencyModelInpout?.Currency;
            this.CurrencyOutput.Description = currencyModelOutpout?.Currency;
            this.CurrencyInput.CurrencySymbol = currencyModelInpout?.Symbol;
            this.CurrencyOutput.CurrencySymbol = currencyModelOutpout?.Symbol;

            this.CurrencyGrid.ReportToCurrency = new CurrencyReporter(inCurrency);
            this.LoadDate = IsoCurrencyCodes.LoadedDate();
        }

        #region "Relays"
        public RelayCommand RevertCommand { get; }
        #endregion


        private IsoCurrencyModel CurrencyInfo(string currency)
        {
            var currencyInfo = AllCurrencies.SingleOrDefault(x => x.ISOCode == currency);
            return currencyInfo;
        }

        public CurrencyConversionViewModel()
        {
            this.AllCurrencies = IsoCurrencyCodes.AllCurrencies().ToList();
            this.InUseCurrencies = AllCurrencies.Where(x => x.InUse == true).ToList();

            ResetDefaults();

            this.CurrencyInput.ValueChanged += InputCurrency;
            this.CurrencyInput.InputValue = 1.00;

            this.CurrencyOutput.IsReadOnly = true;
            this.SelectorFrom.Id = FromSelector;
            this.SelectorFrom.SelectionChanged += SelectionChanged;

            this.SelectorTo.Id = ToSelector;
            this.SelectorTo.SelectionChanged += SelectionChanged;

            RevertCommand = new RelayCommand(this.Revert);

            Mediator.Instance.Register(this);
        }

        private void ResetDefaults()
        {
            var firstFrom = this.SelectorFrom.Currencies.FirstOrDefault();
            var firstTo = this.SelectorTo.Currencies.FirstOrDefault();
            if (firstFrom != null && firstTo != null)
            {
                this.SelectorFrom.DefaultSelection = firstFrom.Currency;
                this.SelectorTo.DefaultSelection = firstTo.Currency;
                SetCurrencies();
            }
        }
        [MediatorConnection("InUseCountMessage")]
        public void MessageReceived(int? message)
        {
            if (message is int && message > 0)
            {
                //reload currency list
                this.SelectorFrom.DefaultSelection = this.SelectorTo.DefaultSelection = null;
                this.SelectorFrom.CurrencySelected = this.SelectorTo.CurrencySelected = null;
                SelectorFrom.RefreshList();
                SelectorTo.RefreshList();
                ResetDefaults();
            }

        }
        private void Revert()
        {
            var currencyFrom = this.SelectorFrom.CurrencySelected?.Currency;
            var currencyTo = this.SelectorTo.CurrencySelected?.Currency;
            this.SelectorFrom.CurrencySelected = new CurrencySelectorItem(currencyTo);
            this.SelectorTo.CurrencySelected = new CurrencySelectorItem(currencyFrom);
        }

        private void SelectionChanged(object sender, CurrencySelectorItem e)
        {
            if (e != null)
            {
                SetCurrencies();
                SetCalculation();
                SetMajorCurrencies(e.Currency);

                if (e.ParentId == FromSelector)
                {
                    this.CurrencySelectedIcon = LoadImage($"{e.Currency}.png");
                    this.CurrencyToCompare = e.Currency;
                }
            }
        }

        private void InputCurrency(object sender, double e)
        {
            this.EnteredValue = e;
            SetCalculation();
        }

        private CurrencySelectorItem _fromCurrency;
        public CurrencySelectorItem FromCurrency
        {
            get { return _fromCurrency; }
            set { SetProperty(ref _fromCurrency, value); }
        }

        private bool _inverseCurrencyCompare;
        public bool InverseCurrencyCompare
        {
            get { return _inverseCurrencyCompare; }
            set
            {
                SetProperty(ref _inverseCurrencyCompare, value);
                SetMajorCurrencies(this.CurrencyGrid.ReportToCurrency.Currency);
            }
        }

        private BitmapImage _currencySelectedIcon;
        public BitmapImage CurrencySelectedIcon
        {
            get { return _currencySelectedIcon; }
            set { SetProperty(ref _currencySelectedIcon, value); }
        }

        private string _currencyToCompare;
        public string CurrencyToCompare
        {
            get { return _currencyToCompare; }
            set { SetProperty(ref _currencyToCompare, value); }
        }
        public CurrencySelectorItem ToCurrency
        {
            get { return new CurrencySelectorItem(this.SelectorTo.CurrencySelected?.Currency); }
        }

        private double _enteredValue;
        public double EnteredValue
        {
            get { return _enteredValue; }
            set { SetProperty(ref _enteredValue, value); }
        }

        public double CalculatedValue
        {
            set { this.CurrencyOutput.InputValue = value; }
        }

        private DateTime? _loadDate;
        public DateTime? LoadDate
        {
            get { return _loadDate; }
            set { SetProperty(ref _loadDate, value); }
        }

        public List<IsoCurrencyModel> AllCurrencies { get; }
        public List<IsoCurrencyModel> InUseCurrencies { get; }

        private void SetMajorCurrencies(string Currency)
        {
            var newReporter = new CurrencyReporter(Currency);
            newReporter.Reporter = this.InverseCurrencyCompare ? RporterType.Reverse : RporterType.Direct;
            this.CurrencyGrid.ReportToCurrency = newReporter;
        }

        private double CalculateConversion()
        {
            string CurrencyIn = this.SelectorFrom.CurrencySelected?.Currency;
            string CurrencyOut = this.SelectorTo.CurrencySelected?.Currency;
            var inputCurrency = AllCurrencies.SingleOrDefault(x => x.ISOCode == CurrencyIn);
            var outputCurrency = AllCurrencies.SingleOrDefault(x => x.ISOCode == CurrencyOut);

            double currencyRate = CurrencyHelper.GetCurrencyRate(inputCurrency, outputCurrency);
            double InputValue = this.EnteredValue;

            return Math.Round(InputValue * currencyRate, 2);
        }

        private void SetCalculation()
        {
            this.CalculatedValue = CalculateConversion();
        }
        private BitmapImage LoadImage(string filename)
        {
            var uri = new Uri("pack://application:,,,/Resources/" + filename, UriKind.Absolute);
            var bitmap = new BitmapImage();

            try
            {
                bitmap = new BitmapImage(uri);
            }
            catch
            {
                var defaultImageUri = new Uri("pack://application:,,,/Resources/EmptyFlag.png", UriKind.Absolute);
                bitmap = new BitmapImage(defaultImageUri);
            }

            return bitmap;
        }

    }
}

