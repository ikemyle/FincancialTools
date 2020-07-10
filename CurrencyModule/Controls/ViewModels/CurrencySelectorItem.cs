using Currency.Core.Helpers;
using CurrencyConverter.Helpers;
using CurrencyConverter.Models;
using CurrencyData.Controllers;
using System;
using System.Linq;
using System.Windows.Media.Imaging;

namespace CurrencyConverter.Controls.ViewModels
{
    public class CurrencySelectorItem : BaseBindable
    {
        public CurrencySelectorItem(string itemCurrency, CurrencyReporter Reporter = null)
        {
            if (Reporter?.Reporter == RporterType.Reverse)
            {
                this.Unit = "1 ";
            }
            Currency = itemCurrency;
            if (Reporter != null)
            {
                var inputCurrency = IsoCurrencyCodes.AllCurrencies().ToList().SingleOrDefault(x => x.ISOCode == itemCurrency);
                var outputCurrency = IsoCurrencyCodes.AllCurrencies().ToList().SingleOrDefault(x => x.ISOCode == Reporter.Currency);
                this.ReporterValue = Math.Round(Reporter.Reporter == RporterType.Direct ? CurrencyHelper.GetCurrencyRate(outputCurrency, inputCurrency) :
                                        CurrencyHelper.GetCurrencyRate(inputCurrency, outputCurrency), 4);
            }
        }

        public string ParentId { get; set; }

        private string _unit;
        public string Unit
        {
            get { return _unit; }
            set { SetProperty(ref _unit, value); }
        }

        private string _currency;
        public string Currency
        {
            get { return _currency; }
            set { SetProperty(ref _currency, value); }
        }

        private double _reporterValue;
        public double ReporterValue
        {
            get { return _reporterValue; }
            set { SetProperty(ref _reporterValue, value); }
        }
        public BitmapImage Icon
        {
            get { return LoadImage($"{Currency}.png"); }
        }
        private BitmapImage LoadImage(string filename)
        {
            var assembly = typeof(CurrencySelectorItem).Assembly;
            var AssemblyName = assembly.GetName().Name;

            var imgUri = String.Format(@"pack://application:,,,/" + AssemblyName + ";component/Resources/" + filename);

            var bitmap = new BitmapImage();

            try
            {
                bitmap = new BitmapImage(new Uri(imgUri, UriKind.RelativeOrAbsolute));
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
