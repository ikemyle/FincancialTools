using Currency.Core.Helpers;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace CurrencyConverter.ViewModels
{
    public class CurrencyEditorModel : BaseBindable, IDataErrorInfo
    {
        private string _isocode;
        public string ISOCode
        {
            get { return _isocode; }
            set { SetProperty(ref _isocode, value); }
        }

        private string _currency;
        public string Currency
        {
            get { return _currency; }
            set { SetProperty(ref _currency, value); }
        }

        private string _base = "EUR";
        public string Base
        {
            get { return _base; }
            set { SetProperty(ref _base, value); }
        }

        private double _rate;
        public double Rate
        {
            get { return _rate; }
            set { SetProperty(ref _rate, value); }
        }

        #region IDataErrorInfo
        //In this region we are implementing the properties defined in //the IDataErrorInfo interface in System.ComponentModel
        public string this[string columnName]
        {
            get
            {
                if ("ValidateInputText" == columnName)
                {
                    if (String.IsNullOrEmpty(ISOCode))
                    {
                        return "Please enter a Currency";
                    }
                }
                else if ("ISOCode" == columnName)
                {
                    Regex rg = new Regex(@"^[a-zA-Z0-9\s,]*$");
                    if (ISOCode == null || !rg.IsMatch(ISOCode))
                    {
                        return "Currency must contain only alfanumeric characters!";
                    }
                }
                return "";
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}

