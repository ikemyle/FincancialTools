using Currency.Core.Helpers;
using System;

namespace CurrencyConverter.Controls.ViewModels
{
    public class CurrencyInputViewModel : BaseBindable
    {
        public event EventHandler<double> ValueChanged;
        private double _inputValue = 0.0;
        public double InputValue
        {
            get { return _inputValue; }
            set
            {
                SetProperty(ref _inputValue, value);
                if (ValueChanged != null) ValueChanged(this, value);
            }
        }

        private string _currencySymbol;
        public string CurrencySymbol
        {
            get { return _currencySymbol; }
            set
            {
                SetProperty(ref _currencySymbol, value);
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                SetProperty(ref _description, value);
            }
        }

        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { SetProperty(ref _isReadOnly, value); }
        }
    }
}
