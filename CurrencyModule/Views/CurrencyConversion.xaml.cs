using CurrencyConverter.Controls;
using CurrencyConverter.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyConverter.Views
{
    /// <summary>
    /// Interaction logic for CurrencyConversion.xaml
    /// </summary>
    public partial class CurrencyConversion : UserControl
    {        
        private CurrencyConversionViewModel CurrencyViewModel = new CurrencyConversionViewModel();
        public CurrencyConversion()
        {
            InitializeComponent();
            this.DataContext = CurrencyViewModel;
            this.CurrencySelectorFrom.DataContext = CurrencyViewModel.SelectorFrom;
            this.CurrencySelectorTo.DataContext = CurrencyViewModel.SelectorTo;
            this.InputFrom.DataContext = CurrencyViewModel.CurrencyInput;
            this.InputTo.DataContext = CurrencyViewModel.CurrencyOutput;
            this.MajorCurrencies.DataContext = CurrencyViewModel.CurrencyGrid;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
