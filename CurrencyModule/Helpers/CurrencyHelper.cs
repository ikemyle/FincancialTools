using CurrencyData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Helpers
{
    public class CurrencyHelper
    {
        public static double GetCurrencyRate(IsoCurrencyModel CurrencyIn, IsoCurrencyModel CurrencyOut)
        {
            if (CurrencyIn == null || CurrencyOut == null)
            {
                return 0;
            }
            double output = 0.0;
            if (CurrencyIn.Base == CurrencyOut.Currency)
            {
                output = 1 / CurrencyIn.Rate;
            }
            else
            {
                if (CurrencyOut.Base == CurrencyIn.Currency)
                {
                    output = CurrencyOut.Rate;
                }
                else
                {
                    output = (CurrencyOut.Rate / CurrencyIn.Rate);
                }
            }
            return output;
        }
    }
}
