using Currency.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Models
{
    public enum RporterType
    {
        Direct,
        Reverse
    }
    public class CurrencyReporter : BaseBindable
    {
        public CurrencyReporter()
        {
                
        }

        public CurrencyReporter(string currency)
        {
            this.Currency = currency;
        }

        private string _currency;
        public string Currency
        {
            get { return _currency; }
            set { SetProperty(ref _currency, value); }
        }

        private RporterType _reporter;
        public RporterType Reporter
        {
            get { return _reporter; }
            set { SetProperty(ref _reporter, value); }
        }
    }
}
